using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;

using Bots.UpdateHandlers;

namespace Bots.TG.UpdateHandlers
{
    public class UpdateHandler : IUpdateHandler, Telegram.Bot.Extensions.Polling.IUpdateHandler
    {
        private Action<InputMessage>? _updateHandler;

        // map[ chatId, queue< respFunc > ]
        private readonly Dictionary<long, Queue<Action<InputMessage>?>> _queriedChatIds;

        private readonly DateTime _startTime;

        public UpdateHandler()
        {
            _queriedChatIds = new Dictionary<long, Queue<Action<InputMessage>?>>();
            _startTime = DateTime.UtcNow;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            await Task.Run(() => Console.WriteLine(errorMessage), CancellationToken.None);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            InputMessage im = new()
            {
                SenderId = update.Message?.Chat.Id ?? -1,
                Text = update.Message?.Text ?? "",
                Time = update.Message?.Date ?? DateTime.UtcNow,
                Username = update.Message?.From?.Username ?? ""
            };

            if (update.Message == null || update.Message.Date < _startTime)
            {
                Console.WriteLine(im.Skipped());
                return;
            }

            if (update.Type != UpdateType.Message)
                return;

            if (update.Message?.Type != MessageType.Text)
                return;

            await HandleInputMessage(im);
        }

        private async Task HandleInputMessage(InputMessage im)
        {
            Console.WriteLine(im.Received());

            if (!_queriedChatIds.ContainsKey(im.SenderId) || _queriedChatIds[im.SenderId].Count == 0)
            {
                await Task.Run(() => { _updateHandler?.Invoke(im); });
            }
            else
            {
                await Task.Run(() => { _queriedChatIds[im.SenderId].Dequeue()?.Invoke(im); });
            }
        }

        public async Task InvokeMessageAsync(long chatId, string message)
        {
            InputMessage im = new()
            {
                SenderId = chatId,
                Text = message,
                Time = DateTime.UtcNow,
                Username = "SYSTEM / To:"
            };

            await HandleInputMessage(im);
        }

        public void AddResponse(Response response)
        {
            _updateHandler += (InputMessage im) =>
            {
                if (response.Filter == null || response.Filter(im))
                {
                    response.UpdateHandler(im);
                }
            };
        }

        public void AddQuery(long chatId, Action<InputMessage> response)
        {
            if (!_queriedChatIds.ContainsKey(chatId))
                _queriedChatIds.Add(chatId, new Queue<Action<InputMessage>?>());

            _queriedChatIds[chatId].Enqueue(response);
        }
    }
}

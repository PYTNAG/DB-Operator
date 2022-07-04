using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

using Bots.UpdateHandlers;

namespace Bots.TG
{
    public class Bot : IBot
    {
        private readonly TelegramBotClient _botClient;
        private readonly CancellationTokenSource _cancellationToken;

        public Data Data { get; }

        private readonly IUpdateHandler _updateHandler;

        public Bot(string token, IUpdateHandler updateHandler, Data data)
        {
            Data = data;
            _botClient = new TelegramBotClient(token);
            _cancellationToken = new CancellationTokenSource();
            _updateHandler = updateHandler;
        }

        public async void SendMessageAsync(
            long chatId,
            string text,
            Bots.Keyboard? keyboard = null
        )
        {
            if (keyboard is not Keyboard && keyboard != null)
                throw new Exception("Not-TG keyboard using for TG bot");

            await _botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: text,
                        replyMarkup: (keyboard as Keyboard)?.GetVerticalReplyMarkup() ?? Keyboard.RemoveMarkup
                    );
        }

        public Task SendSticker(ChatId chatId, string stId)
        {
            var st = new InputTelegramFile(stId);

            if (st.FileId == null)
                throw new Exception("Null file id for sticker");

            return _botClient.SendStickerAsync(chatId, st.FileId);
        }

        public void StartReceiving()
        {
            _botClient.StartReceiving(
                (UpdateHandlers.UpdateHandler)_updateHandler,
                new Telegram.Bot.Extensions.Polling.ReceiverOptions { AllowedUpdates = { } },
                _cancellationToken.Token
                );
        }

        public void StopReceiving()
        {
            _cancellationToken.Cancel();
        }

        public void InvokeMessageAsync(long userId, string mes)
        {
            _updateHandler.InvokeMessageAsync(userId, mes);
        }

        public void AddResponse(Func<InputMessage, bool> filter, Action<InputMessage> response)
        {
            _updateHandler.AddResponse(new Response(filter, response));
        }

        public void AddQuery(long chatId, Action<InputMessage> query)
        {
            _updateHandler.AddQuery(chatId, query);
        }

        public Bots.Keyboard GetKeyboard(params string[] buttonsNames)
        {
            return new Keyboard(buttonsNames);
        }

        public void SendPhotoAsync(long userId, string path, string caption, Bots.Keyboard? keyboard = null)
        {
            throw new NotImplementedException();
        }

        public void SendDocumentAsync(long userId, string path, string caption, Bots.Keyboard? keyboard = null)
        {
            throw new NotImplementedException();
        }

        public void SendStickerAsync(long userId, string set, uint index, Bots.Keyboard? keyboard = null)
        {
            throw new NotImplementedException();
        }
    }
}

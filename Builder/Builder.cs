using Bots;

namespace Builder
{
    public class Builder
    {
        private readonly IBot _bot;
        private readonly Buttons.Buttons _buttons;

        public Builder(IBot bot)
        {
            _bot = bot;
            _buttons = new();
        }

        public void Build()
        {
            _buttons.AddButton("CREATE_QUEUE", "Создать очередь");
            _buttons.AddButton("CONNECT_TO_QUEUE", "Создать очередь");

            AddStartResponse();
            AddCreateQueueQuery();
            AddConnectToQueueQuery();
        }

        private void AddStartResponse()
        {
            _bot.AddResponse(
                    (InputMessage im) => im.Text == "/start",
                    (InputMessage im) =>
                    {
                        _bot.SendMessageAsync(
                                im.SenderId,
                                "Привет! <...>",
                                _bot.GetKeyboard(_buttons["CREATE_QUEUE"].Text, _buttons["CONNECT_TO_QUEUE"].Text)
                            );
                    }
                );
        }

        private void AddCreateQueueQuery()
        {
            _bot.AddResponse(
                    (InputMessage im) => im.Text == _buttons["CREATE_QUEUE"].Text,
                    (InputMessage im) =>
                    {
                        _bot.SendMessageAsync(
                                im.SenderId,
                                "Напишите название новой очереди:",
                                keyboard: _bot.GetKeyboard("Вернуться")
                            );

                        _bot.AddQuery(im.SenderId, (im) => { /* get name, create table and so on... */ });
                    }
                );
        }

        private void AddConnectToQueueQuery()
        {
            _bot.AddResponse(
                    (InputMessage im) => im.Text == _buttons["CONNECT_TO_QUEUE"].Text,
                    (InputMessage im) =>
                    {
                        _bot.SendMessageAsync(
                                im.SenderId,
                                "Код комнаты:",
                                keyboard: _bot.GetKeyboard("Вернуться")
                            );

                        _bot.AddQuery(im.SenderId, (im) => { /* search for room, connect and so on... */ });
                    }
                );
        }
    }
}

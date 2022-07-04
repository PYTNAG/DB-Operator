using Telegram.Bot.Types.ReplyMarkups;

namespace Bots.TG
{
    public class Keyboard : Bots.Keyboard
    {
        public static IReplyMarkup RemoveMarkup
        {
            get
            {
                return new ReplyKeyboardRemove();
            }
        }

        public IReplyMarkup GetVerticalReplyMarkup()
        {
            return new ReplyKeyboardMarkup(GetKeyboardVerticalLayout());
        }

        public Keyboard(params string[] buttonsNames) : base(buttonsNames) { }

        private List<KeyboardButton[]> GetKeyboardVerticalLayout()
        {
            List<KeyboardButton[]> buttons = new();
            for (int i = 0; i < _buttonsNames.Count; ++i)
            {
                KeyboardButton[] button = {
                    new KeyboardButton(_buttonsNames[i])
                };
                buttons.Add(button);
            }

            return buttons;
        }
    }
}

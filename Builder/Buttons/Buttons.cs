namespace Builder.Buttons
{
    public class Buttons
    {
        private readonly HashSet<Button> _buttons;

        public Buttons()
        {
            _buttons = new HashSet<Button>();
        }

        public Button AddButton(string name, string text)
        {
            if (_buttons.Any(btn => btn.Name == name))
                throw new Exception("Button with this name already exist");

            var btn = new Button(name, text);
            _buttons.Add(btn);

            return btn;
        }

        public void RemoveButton(string name)
        {
            _buttons.Remove(_buttons.First(btn => btn.Name == name));
        }

        public Button this[string name]
        {
            get
            {
                Button? res = _buttons.FirstOrDefault(btn => btn.Name == name);
                if (res == null)
                    throw new Exception("Button with this name isn't exist");

                return res;
            }
        }
    }
}

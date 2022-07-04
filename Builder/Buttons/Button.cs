namespace Builder.Buttons
{
    public class Button
    {
        public string Name { get; }
        public string Text { get; }

        public Button(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}

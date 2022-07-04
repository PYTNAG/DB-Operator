using Bots.UpdateHandlers;

namespace Bots
{
    public interface IBot
    {
        public Data Data { get; }

        public void StartReceiving();
        public void StopReceiving();

        public Keyboard GetKeyboard(params string[] buttonsNames);
        public void AddResponse(Func<InputMessage, bool> filter, Action<InputMessage> response);
        public void AddQuery(long chatId, Action<InputMessage> response);

        public void InvokeMessageAsync(long userId, string message);
        public void SendMessageAsync(long chatId, string text, Keyboard? keyboard = null);

        public void SendPhotoAsync(long userId, string path, string caption, Keyboard? keyboard = null);
        public void SendDocumentAsync(long userId, string path, string caption, Keyboard? keyboard = null);
        public void SendStickerAsync(long userId, string set, uint index, Keyboard? keyboard = null);
    }
}

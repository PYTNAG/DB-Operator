using Bots.UpdateHandlers;

namespace Bots
{
    public interface IBot
    {
        Data Data { get; }

        void StartReceiving();
        void StopReceiving();

        Keyboard GetKeyboard(params string[] buttonsNames);
        void AddResponse(Func<InputMessage, bool> filter, Action<InputMessage> response);
        void AddQuery(long chatId, Action<InputMessage> response);

        void InvokeMessageAsync(long userId, string message);
        void SendMessageAsync(long chatId, string text, Keyboard? keyboard = null);

        void SendPhotoAsync(long userId, string path, string caption, Keyboard? keyboard = null);
        void SendDocumentAsync(long userId, string path, string caption, Keyboard? keyboard = null);
        void SendStickerAsync(long userId, string set, uint index, Keyboard? keyboard = null);
    }
}

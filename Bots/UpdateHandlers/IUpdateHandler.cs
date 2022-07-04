namespace Bots.UpdateHandlers
{
    public interface IUpdateHandler
    {
        public Task InvokeMessageAsync(long chatId, string message);

        public void AddResponse(Response response);

        public void AddQuery(long chatId, Action<InputMessage> response);
    }
}

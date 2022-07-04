namespace Bots.UpdateHandlers
{
    public class Response
    {
        public Func<InputMessage, bool>? Filter { get; }
        public Action<InputMessage> UpdateHandler { get; }

        public Response(Func<InputMessage, bool>? filter, Action<InputMessage> updateHandler)
        {
            Filter = filter;
            UpdateHandler = updateHandler;
        }

        public Action<InputMessage> GetWrappedResponse()
        {
            return (InputMessage im) =>
            {
                if (Filter == null || Filter(im))
                {
                    UpdateHandler(im);
                }
            };
        }
    }
}

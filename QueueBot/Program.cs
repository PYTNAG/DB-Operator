using Bots;

using TG = Bots.TG;

namespace QueueBot
{
    internal class Program
    {
        static void Main()
        {
            Data data = new(/* ... */);

            TG.Bot tgbot = new(
                    "your token",
                    new TG.UpdateHandlers.UpdateHandler(),
                    data
                );

            Builder.Builder tgBuilder = new(tgbot);
            tgBuilder.Build();

            tgbot.StartReceiving();

            Console.ReadLine();
        }
    }
}

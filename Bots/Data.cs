using DataBases;

namespace Bots
{
    public struct Session
    {
        public Member Member;
        public Room Room;
        public Team Team;
    }

    public class Data
    {
        public List<Session> Sessions { get; }

        public List<INotificator> Notificators { get; }

        public DataBase DataBase { get; }

        public Data(DataBase db)
        {
            Sessions = new List<Session>();
            DataBase = db;
            Notificators = new List<INotificator>();
        }
    }
}
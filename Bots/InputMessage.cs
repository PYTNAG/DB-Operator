using System;

namespace Bots
{
    public struct InputMessage
    {
        public long SenderId;
        public string Text;

        public DateTime Time;
        public string Username;

        public string Skipped() => $"[ {Time:HH:mm:ss} ] : Skip a message '{Text}' in chat @{Username} [{SenderId}].";
        public string Received() => $"[ {Time:HH:mm:ss} ] : Received a message '{Text}' in chat @{Username} [{SenderId}].";
    }
}

using System.Collections.Generic;
using TheHuntGame.Network.Data;

namespace TheHuntGame.EventSystem.Events
{
    public struct GameStartEvent: IEvent
    {
        public List<AnimalData> Animals;
    }
}
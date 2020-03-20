using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHuntGame.EventSystem.Events
{
    public struct RopeUpdatedEvent : IEvent
    {
        public int NumberOfRopes;
    }
}

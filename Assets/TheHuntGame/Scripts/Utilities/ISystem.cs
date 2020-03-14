using UnityEngine.Events;

namespace TheHuntGame.Utilities
{
    public interface ISystem
    {
        void Initialize();
        UnityAction OnInitializeDone { get; set; }
    }
}
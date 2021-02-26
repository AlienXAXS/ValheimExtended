using System;
using System.Threading.Tasks;

namespace Mod.Events
{
    public class HumanoidEvents
    {
        // Humanoid Awake
        private readonly AsyncEvent<Func<Humanoid, Task>> _humanoidAwakeEvent = new AsyncEvent<Func<Humanoid, Task>>();
        public event Func<Humanoid, Task> HumanoidAwakeEvent
        {
            add => _humanoidAwakeEvent.Add(value);
            remove => _humanoidAwakeEvent.Remove(value);
        }
        public void IsItemEquipableEventInvoke(Humanoid humanoid)
        {
            if (!_humanoidAwakeEvent.HasSubscribers) return;
            foreach (var subscriber in _humanoidAwakeEvent.Subscriptions)
            {
                subscriber.Invoke(humanoid);
            }
        }
    }
}

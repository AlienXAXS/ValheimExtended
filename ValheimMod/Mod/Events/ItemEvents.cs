using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod.Events
{
    public class ItemEvents
    {
        // Is Item Equipable
        private readonly AsyncEvent<Func<bool, ItemDrop.ItemData, bool>> _isItemEquipableEvent = new AsyncEvent<Func<bool, ItemDrop.ItemData, bool>>();
        public event Func<bool, ItemDrop.ItemData, bool> IsItemEquipable
        {
            add => _isItemEquipableEvent.Add(value);
            remove => _isItemEquipableEvent.Remove(value);
        }
        public async Task<bool> IsItemEquipableEventInvoke(bool initialResult, ItemDrop.ItemData itemData)
        {
            if (!_isItemEquipableEvent.HasSubscribers) return initialResult;

            var anyWereTrue = false;

            foreach (var subscriber in _isItemEquipableEvent.Subscriptions)
            {
                var taskResult = subscriber.Invoke(initialResult, itemData);
                if (taskResult)
                    anyWereTrue = true;
            }

            return initialResult || anyWereTrue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod.Events
{
    class InputKeyRouter
    {
        public static InputKeyRouter Instance = _instance ?? (_instance = new InputKeyRouter());
        private static InputKeyRouter _instance;

        private readonly List<KeyCode> _listeningKeyCodes = new List<KeyCode>();

        // Player Spawned
        internal readonly AsyncEvent<Func<KeyCode, Task>> KeyDownEvent = new AsyncEvent<Func<KeyCode, Task>>();
        public event Func<KeyCode, Task> KeyDown
        {
            add => KeyDownEvent.Add(value);
            remove => KeyDownEvent.Remove(value);
        }
        public void KeyDownInvoke(KeyCode inputKeyCode)
        {
            if (!KeyDownEvent.HasSubscribers) return;
            foreach (var subscriber in KeyDownEvent.Subscriptions)
                if ( _listeningKeyCodes.Any(code => code == inputKeyCode) )
                    subscriber.Invoke(inputKeyCode);
        }

        public List<KeyCode> GetKeyCodes()
        {
            return _listeningKeyCodes;
        }

        public void AddListenedKeyCode(KeyCode keyCode)
        {
            _listeningKeyCodes.Add(keyCode);
        }

        public void RemoveListenedKeyCode(KeyCode keyCode)
        {
            _listeningKeyCodes.Remove(keyCode);
        }
    }
}

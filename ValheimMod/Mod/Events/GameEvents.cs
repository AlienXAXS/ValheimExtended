using System;
using System.Threading.Tasks;

namespace Mod.Events
{
    public class GameEvents
    {
        // Server started
        private readonly AsyncEvent<Func<Task>> _serverStartedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> ServerStarted
        {
            add => _serverStartedEvent.Add(value);
            remove => _serverStartedEvent.Remove(value);
        }
        public void ServerStartedInvoke()
        {
            if (!_serverStartedEvent.HasSubscribers) return;
            foreach (var subscriber in _serverStartedEvent.Subscriptions)
                subscriber.Invoke();
        }

        // Game is ready
        private readonly AsyncEvent<Func<Task>> _gameReadyEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> GameReady
        {
            add => _gameReadyEvent.Add(value);
            remove => _gameReadyEvent.Remove(value);
        }
        public void GameReadyInvoke()
        {
            if (!_gameReadyEvent.HasSubscribers) return;
            foreach (var subscriber in _gameReadyEvent.Subscriptions)
                subscriber.Invoke();
        }
    }
}

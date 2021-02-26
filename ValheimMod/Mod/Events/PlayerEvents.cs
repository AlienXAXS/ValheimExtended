using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod.Events
{
    public class PlayerEvents
    {

        // Player Spawned
        private readonly AsyncEvent<Func<Player, Task>> _playerSpawnedEvent = new AsyncEvent<Func<Player, Task>>();
        public event Func<Player, Task> PlayerSpawned
        {
            add => _playerSpawnedEvent.Add(value);
            remove => _playerSpawnedEvent.Remove(value);
        }
        public void PlayerSpawnedInvoke(Player input)
        {
            if (!_playerSpawnedEvent.HasSubscribers) return;
            foreach (var subscriber in _playerSpawnedEvent.Subscriptions)
                subscriber.Invoke(input);
        }

        // Player Death
        private readonly AsyncEvent<Func<Player, Task>> _playerDeathEvent = new AsyncEvent<Func<Player, Task>>();
        public event Func<Player, Task> PlayerDeath
        {
            add => _playerDeathEvent.Add(value);
            remove => _playerDeathEvent.Remove(value);
        }
        public void PlayerDeathEventInvoke(Player input)
        {
            if (!_playerDeathEvent.HasSubscribers) return;
            foreach (var subscriber in _playerDeathEvent.Subscriptions)
                subscriber.Invoke(input);
        }

        // Player Attack
        private readonly AsyncEvent<Func<Player, HitData, Task>> _playerAttackEvent = new AsyncEvent<Func<Player, HitData, Task>>();
        public event Func<Player, HitData, Task> PlayerAttack
        {
            add => _playerAttackEvent.Add(value);
            remove => _playerAttackEvent.Remove(value);
        }
        public void PlayerAttackEventInvoke(Player input, HitData hitData)
        {
            if (!_playerAttackEvent.HasSubscribers) return;
            foreach (var subscriber in _playerAttackEvent.Subscriptions)
                subscriber.Invoke(input, hitData);
        }

        // On Player Update
        private readonly AsyncEvent<Func<Player, Task>> _playerUpdateEvent = new AsyncEvent<Func<Player, Task>>();
        public event Func<Player, Task> PlayerUpdate
        {
            add => _playerUpdateEvent.Add(value);
            remove => _playerUpdateEvent.Remove(value);
        }
        public void PlayerUpdateEventInvoke(Player player)
        {
            if (!_playerUpdateEvent.HasSubscribers) return;
            foreach (var subscriber in _playerUpdateEvent.Subscriptions)
                subscriber.Invoke(player);
        }

        // On Local Player Server Connected
        private readonly AsyncEvent<Func<string, Task>> _localPlayerServerConnectedEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<string, Task> LocalPlayerServerConnected
        {
            add => _localPlayerServerConnectedEvent.Add(value);
            remove => _localPlayerServerConnectedEvent.Remove(value);
        }
        public void LocalPlayerServerConnectedEventInvoke(string input)
        {
            if (!_localPlayerServerConnectedEvent.HasSubscribers) return;
            foreach (var subscriber in _localPlayerServerConnectedEvent.Subscriptions)
                subscriber.Invoke(input);
        }

        // On Local Player Server Disconnected
        private readonly AsyncEvent<Func<Task>> _localPlayerServerDisconnectedEvent = new AsyncEvent<Func<Task>>();

        public event Func<Task> LocalPlayerServerDisconnected
        {
            add => _localPlayerServerDisconnectedEvent.Add(value);
            remove => _localPlayerServerDisconnectedEvent.Remove(value);
        }
        public void LocalPlayerServerDisconnectedEventInvoke()
        {
            if (!_localPlayerServerDisconnectedEvent.HasSubscribers) return;
            foreach (var subscriber in _localPlayerServerDisconnectedEvent.Subscriptions)
                subscriber.Invoke();
        }
    }
}

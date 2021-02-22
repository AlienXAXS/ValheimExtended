using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod.Events
{
    public class EventRouter
    {
        public static EventRouter Instance = _instance ?? (_instance = new EventRouter());
        private static EventRouter _instance;

        internal readonly AsyncEvent<Func<Task>> GameReadyEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> GameReady
        {
            add => GameReadyEvent.Add(value);
            remove => GameReadyEvent.Remove(value);
        }
        public void GameReadyInvoke()
        {
            if (!GameReadyEvent.HasSubscribers) return;
            foreach (var subscriber in GameReadyEvent.Subscriptions)
                subscriber.Invoke();
        }

        // Player Spawned
        internal readonly AsyncEvent<Func<Player, Task>> PlayerSpawnedEvent = new AsyncEvent<Func<Player, Task>>();
        public event Func<Player, Task> PlayerSpawned
        {
            add => PlayerSpawnedEvent.Add(value);
            remove => PlayerSpawnedEvent.Remove(value);
        }
        public void PlayerSpawnedInvoke(Player input)
        {
            if (!PlayerSpawnedEvent.HasSubscribers) return;
            foreach (var subscriber in PlayerSpawnedEvent.Subscriptions)
                subscriber.Invoke(input);
        }

        // Player Death
        internal readonly AsyncEvent<Func<Player, Task>> PlayerDeathEvent = new AsyncEvent<Func<Player, Task>>();
        public event Func<Player, Task> PlayerDeath
        {
            add => PlayerDeathEvent.Add(value);
            remove => PlayerDeathEvent.Remove(value);
        }
        public void PlayerDeathEventInvoke(Player input)
        {
            if (!PlayerDeathEvent.HasSubscribers) return;
            foreach (var subscriber in PlayerDeathEvent.Subscriptions)
                subscriber.Invoke(input);
        }

        // Attack
        internal readonly AsyncEvent<Func<Player, HitData, Task>> PlayerAttackEvent = new AsyncEvent<Func<Player, HitData, Task>>();
        public event Func<Player, HitData, Task> PlayerAttack
        {
            add => PlayerAttackEvent.Add(value);
            remove => PlayerAttackEvent.Remove(value);
        }
        public void PlayerAttackEventInvoke(Player input, HitData hitData)
        {
            if (!PlayerAttackEvent.HasSubscribers) return;
            foreach (var subscriber in PlayerAttackEvent.Subscriptions)
                subscriber.Invoke(input, hitData);
        }

        // On Local Player Server Connected
        internal readonly AsyncEvent<Func<string, Task>> LocalPlayerServerConnectedEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<string, Task> LocalPlayerServerConnected
        {
            add => LocalPlayerServerConnectedEvent.Add(value);
            remove => LocalPlayerServerConnectedEvent.Remove(value);
        }
        public void LocalPlayerServerConnectedEventInvoke(string input)
        {
            if (!LocalPlayerServerConnectedEvent.HasSubscribers) return;
            foreach (var subscriber in LocalPlayerServerConnectedEvent.Subscriptions)
                subscriber.Invoke(input);
        }

        // On Local Player Server Disconnected
        internal readonly AsyncEvent<Func<Task>> LocalPlayerServerDisconnectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> LocalPlayerServerDisconnected
        {
            add => LocalPlayerServerDisconnectedEvent.Add(value);
            remove => LocalPlayerServerDisconnectedEvent.Remove(value);
        }
        public void LocalPlayerServerDisconnectedEventInvoke()
        {
            if (!LocalPlayerServerDisconnectedEvent.HasSubscribers) return;
            foreach (var subscriber in LocalPlayerServerDisconnectedEvent.Subscriptions)
                subscriber.Invoke();
        }

        // On Camera Update
        internal readonly AsyncEvent<Func<GameCamera, float, Task>> CameraUpdateEvent = new AsyncEvent<Func<GameCamera, float, Task>>();
        public event Func<GameCamera, float, Task> CameraUpdate
        {
            add => CameraUpdateEvent.Add(value);
            remove => CameraUpdateEvent.Remove(value);
        }
        public void CameraUpdateEventInvoke(GameCamera camera, float dt)
        {
            if (!CameraUpdateEvent.HasSubscribers) return;
            foreach (var subscriber in CameraUpdateEvent.Subscriptions)
                subscriber.Invoke(camera, dt);
        }

        // On Camera Awake
        internal readonly AsyncEvent<Func<GameCamera, Task>> CameraAwakeEvent = new AsyncEvent<Func<GameCamera, Task>>();
        public event Func<GameCamera, Task> CameraAwake
        {
            add => CameraAwakeEvent.Add(value);
            remove => CameraAwakeEvent.Remove(value);
        }
        public void CameraAwakeEventInvoke(GameCamera camera)
        {
            if (!CameraAwakeEvent.HasSubscribers) return;
            foreach (var subscriber in CameraAwakeEvent.Subscriptions)
                subscriber.Invoke(camera);
        }

        // On Player Update
        internal readonly AsyncEvent<Func<Player, Task>> PlayerUpdateEvent = new AsyncEvent<Func<Player, Task>>();
        public event Func<Player, Task> PlayerUpdate
        {
            add => PlayerUpdateEvent.Add(value);
            remove => PlayerUpdateEvent.Remove(value);
        }
        public void PlayerUpdateEventInvoke(Player player)
        {
            if (!PlayerUpdateEvent.HasSubscribers) return;
            foreach (var subscriber in PlayerUpdateEvent.Subscriptions)
                subscriber.Invoke(player);
        }

    }
}

using System;
using System.Threading.Tasks;

namespace Mod.Events
{
    public class GameCameraEvents
    {
        // On Camera Update
        private readonly AsyncEvent<Func<GameCamera, float, Task>> _cameraUpdateEvent = new AsyncEvent<Func<GameCamera, float, Task>>();
        public event Func<GameCamera, float, Task> CameraUpdate
        {
            add => _cameraUpdateEvent.Add(value);
            remove => _cameraUpdateEvent.Remove(value);
        }
        public void CameraUpdateEventInvoke(GameCamera camera, float dt)
        {
            if (!_cameraUpdateEvent.HasSubscribers) return;
            foreach (var subscriber in _cameraUpdateEvent.Subscriptions)
                subscriber.Invoke(camera, dt);
        }

        // On Camera Awake
        private readonly AsyncEvent<Func<GameCamera, Task>> _cameraAwakeEvent = new AsyncEvent<Func<GameCamera, Task>>();
        public event Func<GameCamera, Task> CameraAwake
        {
            add => _cameraAwakeEvent.Add(value);
            remove => _cameraAwakeEvent.Remove(value);
        }
        public void CameraAwakeEventInvoke(GameCamera camera)
        {
            if (!_cameraAwakeEvent.HasSubscribers) return;
            foreach (var subscriber in _cameraAwakeEvent.Subscriptions)
                subscriber.Invoke(camera);
        }
    }
}

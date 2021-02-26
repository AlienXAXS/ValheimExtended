using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod.Events
{
    public class EventRouter
    {
        public static EventRouter Instance = _instance ?? (_instance = new EventRouter());
        private static readonly EventRouter _instance;

        public GameCameraEvents GameCameraEvents;
        public PlayerEvents PlayerEvents;
        public GameEvents GameEvents;
        public HumanoidEvents HumanoidEvents;
        public ItemEvents ItemEvents;

        public EventRouter()
        {
            GameCameraEvents = new GameCameraEvents();
            PlayerEvents = new PlayerEvents();
            GameEvents = new GameEvents();
            HumanoidEvents = new HumanoidEvents();
            ItemEvents = new ItemEvents();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Events;
using UnityEngine;

namespace Mod.CustomCode
{
    public class FirstPersonMode
    {
        public void Init()
        {
            Utilities.Logger.Log("Init FirstPersonMode");
            EventRouter.Instance.GameCameraEvents.CameraAwake += OnCameraAwake;
            EventRouter.Instance.GameCameraEvents.CameraUpdate += OnCameraUpdate;
        }

        private Task OnCameraUpdate(GameCamera instance, float dt)
        {
			// Dont allow this in menus
			if ( Player.m_localPlayer == null ) return Task.CompletedTask;

            if (instance.m_freeFly)
			{
				instance.UpdateFreeFly(dt);
				instance.UpdateCameraShake(dt);
				return Task.CompletedTask;
			}

			if (Input.GetKeyDown(KeyCode.F8))
			{
                FirstPersonMode.isFirstPerson = !FirstPersonMode.isFirstPerson;
                ApplyFirstPersonMode(instance);
            }

			instance.m_camera.fieldOfView = instance.m_fov;
			instance.m_skyCamera.fieldOfView = instance.m_fov;

			Player localPlayer = Player.m_localPlayer;
            if (!localPlayer) return Task.CompletedTask;

            if ((!Chat.instance || !Chat.instance.HasFocus()) && !global::Console.IsVisible() && !InventoryGui.IsVisible() && !StoreGui.IsVisible() && !Menu.IsVisible() && !Minimap.IsOpen() && !localPlayer.InCutscene() && !localPlayer.InPlaceMode())
            {
                if (FirstPersonMode.isFirstPerson)
                {
                    if (Input.GetKeyDown(KeyCode.F9))
                    {
                        instance.m_fov += 1f;
                        MessageHud.instance.ShowMessage(MessageHud.MessageType.TopLeft,
                            $"Changed fov to: {instance.m_fov}", 0, null);
                    }
                    else if (Input.GetKeyDown(KeyCode.F10))
                    {
                        instance.m_fov -= 1f;
                        MessageHud.instance.ShowMessage(MessageHud.MessageType.TopLeft,
                            $"Changed fov to: {instance.m_fov}", 0, null);
                    }
                }
            }
            if (localPlayer.IsDead() && localPlayer.GetRagdoll())
            {
                FirstPersonMode.isFirstPerson = false;
                ApplyFirstPersonMode(instance);
            }
            else if (FirstPersonMode.isFirstPerson)
            {
                instance.transform.position = localPlayer.m_head.position;
            }
            instance.UpdateCameraShake(dt);

            return Task.CompletedTask;
        }

        private void ApplyFirstPersonMode(GameCamera instance)
        {
			if (FirstPersonMode.isFirstPerson)
            {
                FirstPersonMode.old_Offset = instance.m_3rdOffset;
                instance.m_3rdOffset = new Vector3(0f, 0f, 0f);
                instance.m_minDistance = 0f;
                instance.m_maxDistance = 0f;
                instance.m_zoomSens = 0f;
                instance.m_nearClipPlaneMax = 0.2f;
                instance.m_nearClipPlaneMin = 0.1f;
                instance.m_fov = 90f;
            }
            else
            {
                instance.m_3rdOffset = FirstPersonMode.old_Offset;
                instance.m_minDistance = FirstPersonMode.minDist;
                instance.m_maxDistance = FirstPersonMode.maxDist;
                instance.m_zoomSens = FirstPersonMode.zoomSens;
                instance.m_fov = 65f;
            }
		}

        private Task OnCameraAwake(GameCamera instance)
        {
            FirstPersonMode.minDist = instance.m_minDistance;
            FirstPersonMode.maxDist = instance.m_maxDistance;
            FirstPersonMode.zoomSens = instance.m_zoomSens;
            instance.m_fpsOffset = Vector3.zero;

            return Task.CompletedTask;
        }

        public static bool isFirstPerson = false;
        public static Vector3 old_Offset = Vector3.zero;
        public static float zoomSens = 10f;
        public static float minDist = 1f;
        public static float maxDist = 8f;
    }
}

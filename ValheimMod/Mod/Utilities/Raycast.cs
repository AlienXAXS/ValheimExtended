using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod.Utilities
{
    class Raycast
    {
        public static bool FindPiece (out Vector3 point, out Vector3 normal, out Piece piece, out Heightmap heightmap, out Collider waterSurface, bool water)
        {
            int layerMask = Player.m_localPlayer.m_placeRayMask;

            if (water)
            {
                layerMask = Player.m_localPlayer.m_placeWaterRayMask;
            }

            RaycastHit raycastHit;
            if (Physics.Raycast(GameCamera.instance.transform.position, 
                GameCamera.instance.transform.forward,
                out raycastHit, 
                50f, 
                layerMask) && 
                raycastHit.collider && 
                !raycastHit.collider.attachedRigidbody)
            {
                point = raycastHit.point;
                normal = raycastHit.normal;
                piece = raycastHit.collider.GetComponentInParent<Piece>();
                heightmap = raycastHit.collider.GetComponent<Heightmap>();
                waterSurface = raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Water") ? raycastHit.collider : null;
                return true;
            }
            point = Vector3.zero;
            normal = Vector3.zero;
            piece = (Piece)null;
            heightmap = (Heightmap)null;
            waterSurface = (Collider)null;
            return false;
        }

    }
}

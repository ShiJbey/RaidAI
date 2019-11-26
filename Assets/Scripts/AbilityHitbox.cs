using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    /// <summary>
    /// Defines the parameters for the hitbox cast to be done when using a skill.
    /// </summary>
    [CreateAssetMenu(menuName = "Actor/Skills/Hitbox")]
    public class AbilityHitbox : ScriptableObject
    {
        public Vector3 m_halfExt;
        public float m_maxDistance;
        public Vector3 m_centerOffset;
        public int[] m_layers;
        public float m_timeToLive;

        public int GetLayerMask()
        {
            int layerMask = 0;
            for (int i = 0; i < m_layers.Length; i++)
            {
                layerMask = layerMask | (1 << m_layers[i]); 
            }
            return layerMask;
        }
    }

    [System.Serializable]
    public class HitboxGizmo
    {

        float m_timeToLive;
        RaycastHit m_hit;
        AbilityHitbox m_hitbox;

        public float TimeToLive { get { return m_timeToLive;  } } 

        public HitboxGizmo(RaycastHit hit, AbilityHitbox hitbox)
        {
            m_timeToLive = hitbox.m_timeToLive;
            m_hit = hit;
            m_hitbox = hitbox;
        }

        public void Update(float elapsedTime)
        {
            m_timeToLive -= elapsedTime;
        }

        public void Draw(Transform origin, Matrix4x4 mat)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = mat;
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(m_hitbox.m_centerOffset, origin.forward * m_hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(m_hitbox.m_centerOffset + origin.forward * m_hit.distance, m_hitbox.m_halfExt * 2);
        }
    }
}

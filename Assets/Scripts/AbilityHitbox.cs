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
}

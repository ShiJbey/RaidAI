using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [CreateAssetMenu(menuName = "Actor/Skills/Hitbox")]
    public class AbilityHitbox : ScriptableObject
    {
        public Vector3 m_halfExt;
        public float m_maxDistance;
        public Vector3 m_centerOffset;
        public int[] m_layers;   
    }
}

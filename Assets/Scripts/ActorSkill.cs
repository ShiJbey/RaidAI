using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    [CreateAssetMenu (menuName = "Actor/Skills/ActorSkill")]
    public class ActorSkill : ScriptableObject
    {
        public string m_skillName;
        public ActorStat m_energyCost;
        public ActorStat m_manaCost;
        public ActorStat m_cooldown;
        public Element m_element;
        public AbilityHitbox m_hitbox;

        // Applies the current attack to the target
        public void Activate(Actor target)
        {

        }
    }

    public enum Element
    {
        None,
        Fire,
        Water,
        Air,
        Earth
    }

    public enum SkillType
    {
        Physical,
        Magic
    }
}
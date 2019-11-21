using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    /// <summary>
    /// <c>Skills</c> are used by <c>Actors</c> to attack 
    /// and apply buffs to other <c>Actors</c>
    /// </summary>
    [CreateAssetMenu (menuName = "Actor/Skills/ActorSkill")]
    public class ActorSkill : ScriptableObject
    {
        public string m_skillName;
        public ActorStat m_energyCost;
        public ActorStat m_manaCost;
        public SkillCooldown m_cooldown;
        public Element m_element;
        public AbilityHitbox m_hitbox;
        public float m_damage;
        public StatTarget m_statTarget;
        public StatModifier m_effect;

        /// <summary>
        /// Activates this skill
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        public void Activate(Actor user, Actor target)
        {
            // Applies the energy cost to the player
            user.m_energy.baseValue -= m_energyCost.Value;
            user.m_mana.baseValue -= m_manaCost.Value;

            // Applies the effect to the target
            target.m_health.ApplyDamage(m_damage);

            switch (m_statTarget)
            {
                case StatTarget.Health:
                    target.m_health.AddModifier(m_effect);
                    break;
                case StatTarget.Mana:
                    target.m_mana.AddModifier(m_effect);
                    break;
                case StatTarget.Energy:
                    target.m_energy.AddModifier(m_effect);
                    break;
                case StatTarget.Attack:
                    target.m_attack.AddModifier(m_effect);
                    break;
                case StatTarget.Defense:
                    target.m_defense.AddModifier(m_effect);
                    break;
            }

            // Sets the cooldown
            m_cooldown.ResetCooldown();
            m_cooldown.Active = true;
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

    public enum StatTarget
    {
        Health,
        Mana,
        Energy,
        Attack,
        Defense
    }
}
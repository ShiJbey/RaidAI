using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RaidAI
{
    [System.Serializable]
    public class HealthStat : ActorStat
    {

        public float m_damage = 0f;
        private float m_lastDamage = 0f;
        private float m_maxValue = 0f;

        public HealthStat(float value) : base(value)
        {

        }

        public float MaxValue
        {
            get
            {
                if (isDirty || lastBaseValue != baseValue || m_lastDamage != m_damage)
                {
                    if (m_damage < 0) m_damage = 0;
                    m_lastDamage = m_damage;
                    lastBaseValue = baseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }
                return m_maxValue;
            }
        }

        public override float Value
        {
            get
            {
                if (isDirty || lastBaseValue != baseValue || m_lastDamage != m_damage)
                {
                    if (m_damage < 0) m_damage = 0;
                    m_lastDamage = m_damage;
                    lastBaseValue = baseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }
                return value;
            }
        }

        protected override float CalculateFinalValue()
        {
            m_maxValue = base.CalculateFinalValue();
            float finalValue = m_maxValue - m_damage;
            return (float)Math.Round(finalValue, 4);
        }

        public void HealAllDamage()
        {
            m_damage = 0;
            isDirty = true;
        }

        public void HealDamage(float amount)
        {
            m_damage -= amount;
            isDirty = true;
        }

        public void ApplyDamage(float amount)
        {
            m_damage += amount;
            isDirty = true;
        }
    }
}

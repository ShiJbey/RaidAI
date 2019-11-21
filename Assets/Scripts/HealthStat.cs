using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RaidAI
{
    [System.Serializable]
    public class HealthStat : ActorStat
    {
        private float m_damage = 0f;

        public HealthStat(float value) : base(value)
        {

        }

        public override float Value
        {
            get
            {
                if (isDirty || lastBaseValue != baseValue)
                {
                    lastBaseValue = baseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }
                return value;
            }
        }

        protected override float CalculateFinalValue()
        {
            float finalValue = base.CalculateFinalValue();
            finalValue -= m_damage;
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

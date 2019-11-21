using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [System.Serializable]
    public class SkillCooldown : ActorStat
    {
        public float m_timeLeft = 0;
        private bool m_active = false;

        public SkillCooldown(float value): base(value)
        {

        }

        public bool Active
        {
            get
            {
                return m_active;
            }
            set
            {
                m_active = value;
            }
        }

        public void ResetCooldown()
        {
            m_timeLeft = Value;
        }

        public void DecrementCooldown(float elapsedTime)
        {
            m_timeLeft -= elapsedTime;
            if (m_timeLeft <= 0)
            {
                m_active = false;
                ResetCooldown();
            }
        }
    }
}

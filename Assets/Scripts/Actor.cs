using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    /// <summary>
    /// <c>Actor</c> class defines base functionality for all
    /// the Characters in the game which use AI.
    /// </summary>
    public abstract class Actor : Agent
    {
        // Reference to other commonly used components
        protected Rigidbody m_rBody;

        // Stats
        public HealthStat m_health = new HealthStat(100f);
        public ActorStat m_mana = new ActorStat(100f);
        public ActorStat m_energy = new ActorStat(100f);
        public ActorStat m_attack = new ActorStat(10f);
        public ActorStat m_defense = new ActorStat(10f);

        // Movement
        public float m_rotateSpeed = 1f;
        public float m_movementSpeed = 20f;
    
        // Skills
        public ActorSkill[] m_skills;

        // Arena reference for respawning, etc
        public RaidArena m_raidArena;

        private void Update()
        {
            DecrementSkillCooldowns(Time.deltaTime);
        }

        public override void InitializeAgent()
        {
            base.InitializeAgent();

            // Reset rotation
            transform.rotation = Quaternion.identity;

            // Reset the rigid body
            m_rBody = GetComponent<Rigidbody>();
            m_rBody.angularVelocity = Vector3.zero;
            m_rBody.velocity = Vector3.zero;
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            // Check if the actor is dead
            if (IsAlive())
            {
                SetReward(-1.0f);
                Done();
            }
        }

        public List<float> GetCompleteState()
        {
            List<float> state = new List<float>();
            state.Add(transform.position.x);
            state.Add(transform.position.y);
            state.Add(transform.position.z);
            state.Add(m_health.Value);
            state.Add(m_mana.Value);
            state.Add(m_energy.Value);
            state.Add(m_attack.Value);
            state.Add(m_defense.Value);
            return state;
        }

        public List<float> GetPartialState()
        {
            List<float> state = new List<float>();
            state.Add(transform.position.x);
            state.Add(transform.position.y);
            state.Add(transform.position.z);
            state.Add(m_health.Value);
            return state;
        }

        /// <summary>
        /// Returns true if the Actor stil has health points
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return m_health.Value > 0;
        }

        /// <summary>
        /// Decrements the timer for all active cooldowns on this
        /// actor.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void DecrementSkillCooldowns(float deltaTime)
        {
            // Increment the timers
            for (int i = 0; i < m_skills.Length; i++)
            {
                ActorSkill skill = m_skills[i];
                if (skill.m_cooldown.Active)
                {
                    skill.m_cooldown.DecrementCooldown(deltaTime);
                }
            }
        }

        /// <summary>
        /// Can this Actor use the given skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool CanUseSkill(ActorSkill skill)
        {
            return (m_energy.Value >= skill.m_energyCost.Value &&
                m_mana.Value >= skill.m_manaCost.Value);
        }

        /// <summary>
        /// Activates one of this actor's skills
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="target"></param>
        public void UseSkill(ActorSkill skill, Actor target)
        {
            skill.Activate(this, target);
        }

        /// <summary>
        /// Rotates the character about the y-axis;
        /// </summary>
        /// <param name="axisValue"></param>
        public void RotateActor(float axisValue)
        {
            Vector3 rotateVector = new Vector3(0f, 1f, 0f);
            rotateVector *= Time.deltaTime * m_rotateSpeed * axisValue;
            this.transform.rotation *= Quaternion.Euler(rotateVector);
        }

        /// <summary>
        /// Moves the Character forward and backward.
        /// </summary>
        /// <param name="axisValue"></param>
        public void MoveActor(float axisValue)
        {
            Vector3 movementVector = this.transform.forward.normalized * axisValue;
            this.transform.position += Time.deltaTime * m_movementSpeed * movementVector;
        }
    }
}
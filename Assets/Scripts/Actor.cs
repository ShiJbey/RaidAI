using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    public abstract class Actor : Agent
    {
        protected Rigidbody m_rBody;

        // Stats
        public ActorStat health = new ActorStat(100f);
        public ActorStat mana = new ActorStat(100f);
        public ActorStat energy = new ActorStat(100f);
        public ActorStat attack = new ActorStat(10f);
        public ActorStat defense = new ActorStat(10f);

        // Movement
        public float rotateSpeed = 1f;
        public float movementSpeed = 20f;
    
        // Skills
        public List<ActorSkill> skills;

        // Arena reference for respawning, etc
        public RaidArena m_raidArena;

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
            if (health.Value <= 0.0f)
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
            state.Add(health.Value);
            state.Add(mana.Value);
            state.Add(energy.Value);
            state.Add(attack.Value);
            state.Add(defense.Value);
            return state;
        }

        public List<float> GetPartialState()
        {
            List<float> state = new List<float>();
            state.Add(transform.position.x);
            state.Add(transform.position.y);
            state.Add(transform.position.z);
            state.Add(health.Value);
            return state;
        }

        public bool IsAlive()
        {
            return health.Value > 0;
        }

        // Using Skills
        public void UseSkill(ActorSkill skill, Actor target)
        {
            skill.Activate(target);
        }

        // Movement
        public void RotateActor(float axisValue)
        {
            Vector3 rotateVector = new Vector3(0f, 1f, 0f);
            rotateVector *= Time.deltaTime * rotateSpeed * axisValue;
            this.transform.rotation *= Quaternion.Euler(rotateVector);
        }

        public void MoveActor(float axisValue)
        {
            Vector3 movementVector = this.transform.forward.normalized * axisValue;
            this.transform.position += Time.deltaTime * movementSpeed * movementVector;
        }
    }
}
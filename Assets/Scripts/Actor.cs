using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    public abstract class Actor : Agent
    {
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
        public RaidArena raidArena;

        // Update is called once per frame
        void Update()
        {
            // Check if the actor is dead
            if (health.Value <= 0.0f)
            {
                SetReward(-1.0f);
                Done();
            }
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
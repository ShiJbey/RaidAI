using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    public abstract class Actor : Agent
    {
        // Stats
        [HideInInspector]
        public ActorStat health = new ActorStat(100f);

        // Movement
        public float rotateSpeed = 1f;
        public float movementSpeed = 20f;
    
        // Skills
        private List<ActorSkill> skills;


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
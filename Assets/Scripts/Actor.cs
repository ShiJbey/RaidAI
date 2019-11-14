using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    public abstract class Actor : Agent
    {
        // Stats
        private float baseLife;
        private float baseMana;
        private float baseAttack;
        private float baseDefense;

        // Current Stats
        public float health = 100.0f;
        public float mana = 100.0f;
        public float attack = 100.0f;
        public float defense = 100.0f;
        public float speed = 100.0f;

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
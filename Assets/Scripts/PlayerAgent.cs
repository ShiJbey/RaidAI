using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

namespace RaidAI
{
    public class PlayerAgent : Actor
    {
        public PlayerClass m_playerClass;
        public Transform m_boss;

        
        private Material m_Material;
        private RaycastHit m_Hit;
        private bool m_HitDetect;
        private float m_MaxDistance;


        public override void InitializeAgent()
        {
            base.InitializeAgent();

            // Set the stats using the PlayerClass
            health.baseValue = m_playerClass.health.baseValue;
            mana.baseValue = m_playerClass.mana.baseValue;
            energy.baseValue = m_playerClass.energy.baseValue;
            attack.baseValue = m_playerClass.attack.baseValue;
            defense.baseValue = m_playerClass.defense.baseValue;
            
            // Set the agent color
            m_Material = GetComponent<Renderer>().material;
            m_Material.color = m_playerClass.color;

            // Reset rotation
            transform.rotation = Quaternion.identity;

            // Reset the rigid body
            m_rBody = GetComponent<Rigidbody>();
            m_rBody.angularVelocity = Vector3.zero;
            m_rBody.velocity = Vector3.zero;

            // Box Caster
            m_MaxDistance = 2.0f;
        }

        public override void AgentReset()
        {
            // Reset the agent's health points
            health.RemoveAllModifiers();

            // Find a respawn point
            SpawnPoint point = m_raidArena.spawnManager.GetAvailableSpawn();
            if (point != null)
            {
                // Make sure no-one can spawn below us
                point.Available = false;

                // Reset rotation
                transform.position = point.transform.position;
                transform.rotation = Quaternion.identity;

                // Reset the rigid body
                m_rBody = GetComponent<Rigidbody>();
                m_rBody.angularVelocity = Vector3.zero;
                m_rBody.velocity = Vector3.zero;
                m_MaxDistance = 2.0f;
            }
        }

        public override float[] Heuristic()
        {
            var action = new float[3];
            // Rotate around the y-axis
            action[0] = Input.GetAxis("Horizontal");
            // Move forward or backward
            action[1] = Input.GetAxis("Vertical");

            // Which move to use
            if (Input.GetKeyUp(KeyCode.Alpha0))
            {
                action[2] = 0;
            }
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                action[2] = 1;
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                action[2] = 2;
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                action[2] = 3;
            }
            
            return action;
        }

        public override void CollectObservations()
        {
            AddVectorObs(GetCompleteState());  // 8
            AddVectorObs(m_raidArena.GetTeammateStates(this, false)); // 4 * 3
            AddVectorObs(m_boss.GetComponent<BossAgent>().GetPartialState()); // 4 
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            

            // Actions, size = 2
            MoveActor(vectorAction[1]);
            RotateActor(vectorAction[0]);

            if (vectorAction[2] != 0f)
            {
                // Fire the laser
                int layerMask = 1 << 9;
                Vector3 boxCastHalfExt = new Vector3(0.5f, 0.5f, 0.5f);
                m_HitDetect = Physics.BoxCast(transform.position, boxCastHalfExt, transform.forward, out m_Hit, transform.rotation, m_MaxDistance, layerMask);
                if (m_HitDetect)
                {
                    if (m_Hit.collider.tag == "Boss")
                    {
                        Debug.Log("Attacking boss");
                        m_Hit.collider.GetComponent<BossAgent>().health.AddModifier(new StatModifier(-attack.Value, StatModifier.StatModType.Flat));
                        m_Hit.collider.GetComponent<BossAgent>().Aggro = transform;
                        SetReward(0.5f);
                        return;
                    }
                    
                }
            }

            // Rewards
            float distanceToTarget = Vector3.Distance(transform.position,
                                                      m_boss.position);

            // Reached target
            if (distanceToTarget < 2.7f)
            {
                SetReward(1.0f);
                Done();
                return;
            }

            // Fell off platform
            if (transform.position.y < 0)
            {
                SetReward(-1.0f);
                Done();
                return;
            }

            SetReward(-1.0f);

            // Check if the actor is dead
            if (health.Value <= 0.0f)
            {
                SetReward(-1.0f);
                Done();
            }

            base.AgentAction(vectorAction, textAction);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            //Check if there has been a hit yet
            if (m_HitDetect)
            {
                //Draw a Ray forward from GameObject toward the hit
                Gizmos.DrawRay(transform.position, transform.forward.normalized * m_Hit.distance);
                //Draw a cube that extends to where the hit exists
                Gizmos.DrawWireCube(transform.position + transform.forward.normalized * m_Hit.distance, transform.localScale);
            }
            //If there hasn't been a hit yet, draw the ray at the maximum distance
            else
            {
                //Draw a Ray forward from GameObject toward the maximum distance
                Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
                //Draw a cube at the maximum distance
                Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
            }
        }
    }
}

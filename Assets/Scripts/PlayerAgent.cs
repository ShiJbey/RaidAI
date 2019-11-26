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


        public override void InitializeAgent()
        {
            base.InitializeAgent();

            // Set the stats using the PlayerClass
            m_health.baseValue = m_playerClass.health.baseValue;
            m_mana.baseValue = m_playerClass.mana.baseValue;
            m_energy.baseValue = m_playerClass.energy.baseValue;
            m_attack.baseValue = m_playerClass.attack.baseValue;
            m_defense.baseValue = m_playerClass.defense.baseValue;
            
            // Set the agent color
            m_Material = GetComponent<Renderer>().material;
            m_Material.color = m_playerClass.color;

            // Reset rotation
            transform.rotation = Quaternion.identity;

            // Reset the rigid body
            m_rBody = GetComponent<Rigidbody>();
            m_rBody.angularVelocity = Vector3.zero;
            m_rBody.velocity = Vector3.zero;
        }

        public override void AgentReset()
        {
            // Reset the agent's health points
            m_health.RemoveAllModifiers();

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
                // if vectorAction[2] == 0 then we dont want to attack
                // so attacks are mapped to the interval [1,infinity)
                int actionIndex = Mathf.RoundToInt(vectorAction[2]) - 1;

                // Check that the skill exists
                if (actionIndex < 0 || actionIndex >= m_skills.Length)
                {
                    // Punish for trying to use a move that doesnt exist
                    AddReward(-0.5f);
                    return;
                }

                ActorSkill skill = m_skills[actionIndex];

                // Check if this action has an active cooldown
                if (skill.m_cooldown.Active)
                {
                    // Punish for trying to use a move that is in cooldown
                    AddReward(-0.3f);
                    return;
                }

                // Perform a boxcast with the hitbox for this skill
                RaycastHit hit;
                bool hitDetect = Physics.BoxCast(transform.position + skill.m_hitbox.m_centerOffset,
                    skill.m_hitbox.m_halfExt, transform.forward, out hit, transform.rotation,
                    skill.m_hitbox.m_maxDistance, skill.m_hitbox.GetLayerMask());

                // Create a new hitbox gizmo
                m_activeHitboxes.Add(new HitboxGizmo(hit, skill.m_hitbox));


                if (hitDetect)
                {
                    if (hit.collider.tag == "Boss")
                    {
                        Debug.Log("Attacking boss");
                        hit.collider.GetComponent<BossAgent>().m_health.ApplyDamage(-m_attack.Value);
                        hit.collider.GetComponent<BossAgent>().Aggro = transform;
                        SetReward(1.0f);
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
            if (transform.position.y < -1f)
            {
                SetReward(-1.0f);
                Done();
                return;
            }

            SetReward(-1.0f);

            // Check if the actor is dead
            if (m_health.Value <= 0.0f)
            {
                SetReward(-1.0f);
                Done();
                return;
            }
        }

        void OnDrawGizmos()
        {
            // Draw ray indicating the direction the actor is facing
            DrawForwardRay();
        }

        void DrawForwardRay()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward.normalized * transform.localScale.z * 2);
        }

        void OnDrawGizmosSelected()
        {
            // Draw Hit boxes for used skills
            DrawHitboxes();
        }
    }
}

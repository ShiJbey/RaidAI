using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

namespace RaidAI
{
    public class PlayerAgent : Actor
    {
        public Transform boss;
        private Rigidbody rBody;
        Collider m_Collider;
        RaycastHit m_Hit;
        bool m_HitDetect;
        float m_MaxDistance;


        public override void InitializeAgent()
        {
            rBody = GetComponent<Rigidbody>();
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            m_Collider = GetComponent<Collider>();
            m_MaxDistance = 2.0f;
        }

        public override void AgentReset()
        {
            health.baseValue = 100;
            SpawnPoint point = raidArena.spawnManager.GetAvailableSpawn();
            if (point != null)
            {
                point.Available = false;
                this.rBody.angularVelocity = Vector3.zero;
                this.rBody.velocity = Vector3.zero;
                this.transform.position = point.transform.position;
            }
        }

        public override float[] Heuristic()
        {
            var action = new float[3];
            action[0] = Input.GetAxis("Horizontal");
            action[1] = Input.GetAxis("Vertical");
            action[2] = Input.GetButtonUp("Fire1") ? 1.0f : 0.0f ;
            return action;
        }

        public override void CollectObservations()
        {
            AddVectorObs(this.transform.position);
            AddVectorObs(this.transform.rotation.eulerAngles.y);
            AddVectorObs(this.boss.position);
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
                m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, boxCastHalfExt, transform.forward, out m_Hit, transform.rotation, m_MaxDistance, layerMask);
                if (m_HitDetect)
                {
                    if (m_Hit.collider.tag == "Boss")
                    {
                        Debug.Log("Attacking boss");
                        m_Hit.collider.GetComponent<BossAgent>().health.baseValue -= attack.Value;
                        m_Hit.collider.GetComponent<BossAgent>().Aggro = transform;
                        SetReward(0.5f);
                        return;
                    }
                    
                }
            }

            // Rewards
            float distanceToTarget = Vector3.Distance(this.transform.position,
                                                      boss.position);

            // Reached target
            if (distanceToTarget < 2.7f)
            {
                SetReward(1.0f);
                Done();
                return;
            }

            // Fell off platform
            if (this.transform.position.y < 0)
            {
                SetReward(-1.0f);
                Done();
                return;
            }

            SetReward(-1.0f);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            //Check if there has been a hit yet
            if (m_HitDetect)
            {
                //Draw a Ray forward from GameObject toward the hit
                Gizmos.DrawRay(m_Collider.bounds.center, transform.forward.normalized * m_Hit.distance);
                //Draw a cube that extends to where the hit exists
                Gizmos.DrawWireCube(m_Collider.bounds.center + transform.forward.normalized * m_Hit.distance, transform.localScale);
            }
            //If there hasn't been a hit yet, draw the ray at the maximum distance
            else
            {
                //Draw a Ray forward from GameObject toward the maximum distance
                Gizmos.DrawRay(m_Collider.bounds.center, transform.forward * m_MaxDistance);
                //Draw a cube at the maximum distance
                Gizmos.DrawWireCube(m_Collider.bounds.center + transform.forward * m_MaxDistance, transform.localScale);
            }
        }
    }
}

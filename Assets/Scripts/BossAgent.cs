using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

namespace RaidAI
{
    public class BossAgent : Actor
    {
        private Transform m_aggro;
        private RaycastHit m_Hit;
        private bool m_HitDetect;

        public Transform Aggro
        {
            get
            {
                return m_aggro;
            }
            set
            {
                m_aggro = value;
            }
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

        

        public override void AgentReset()
        {
            m_health.RemoveAllModifiers();

            // Reset transform
            transform.position = new Vector3(0f, 3f, 0f);
            transform.rotation = Quaternion.identity;

            // Reset the rigid body
            m_rBody = GetComponent<Rigidbody>();
            m_rBody.angularVelocity = Vector3.zero;
            m_rBody.velocity = Vector3.zero;
        }

        public override float[] Heuristic()
        {
            float[] action =  { 0f, 0f, 0f };
            return action;
        }

        public override void CollectObservations()
        {
            AddVectorObs(GetCompleteState());
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            // Actions, size = 2
            MoveActor(vectorAction[1]);
            RotateActor(vectorAction[0]);

            // Rewards
            if (m_aggro)
            {
                float distanceToTarget = Vector3.Distance(transform.position,
                                                      m_aggro.position);

                // Reached target
                if (distanceToTarget < 2.7f)
                {
                    SetReward(1.0f);
                    return;
                }
            }

            // Fell off platform
            if (transform.position.y < 0)
            {
                SetReward(-1.0f);
                Done();
                return;
            }

            SetReward(-1.0f);
        }
    }
}

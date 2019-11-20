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
        private float m_MaxDistance;

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

            // Box caster
            m_MaxDistance = 2.0f;
        }

        

        public override void AgentReset()
        {
            health.RemoveAllModifiers();

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
            base.AgentAction(vectorAction, textAction);

            // Actions, size = 2
            MoveActor(vectorAction[1]);
            RotateActor(vectorAction[0]);

            if (vectorAction[2] != 0f)
            {
                // Fire the laser
                int layerMask = 1 << 8;
                Vector3 boxCastHalfExt = new Vector3(0.5f, 0.5f, 0.5f);
                m_HitDetect = Physics.BoxCast(transform.position, boxCastHalfExt, transform.forward, out m_Hit, transform.rotation, m_MaxDistance, layerMask);
                if (m_HitDetect)
                {
                    if (m_Hit.collider.tag == "Player")
                    {
                        Debug.Log("Attacking Player");
                        m_Hit.collider.GetComponent<PlayerAgent>().health.baseValue -= attack.Value;
                        SetReward(0.5f);
                        return;
                    }

                }
            }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

namespace RaidAI
{
    public class PlayerAgent : Actor
    {
        private Rigidbody rBody;
        private Transform boss;

        // Start is called before the first frame update
        void Start()
        {
            rBody = GetComponent<Rigidbody>();
            boss = GameObject.FindGameObjectWithTag("Boss").transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void AgentReset()
        {
            // Select a new spawn point
            if (this.transform.position.y < 0)
            {
                this.rBody.angularVelocity = Vector3.zero;
                this.rBody.velocity = Vector3.zero;
                this.transform.position = new Vector3(0f, 0.5f, 0f);
            }
        }

        public override float[] Heuristic()
        {
            var action = new float[2];
            action[0] = Input.GetAxis("Horizontal");
            action[1] = Input.GetAxis("Vertical");
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

            // Rewards
            float distanceToTarget = Vector3.Distance(this.transform.position,
                                                      boss.position);

            // Reached target
            if (distanceToTarget < 1.42f)
            {
                SetReward(1.0f);
                Done();
            }

            // Fell off platform
            if (this.transform.position.y < 0)
            {
                Done();
            }
        }

        public override void AgentOnDone()
        {
            base.AgentOnDone();
        }
    }
}

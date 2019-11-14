using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

namespace RaidAI
{
    public class BossAgent : Actor
    {
        private Rigidbody rBody;

        // Start is called before the first frame update
        void Start()
        {
            rBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void AgentReset()
        {
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
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            // Actions, size = 2
            MoveActor(vectorAction[1]);
            RotateActor(vectorAction[0]);

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

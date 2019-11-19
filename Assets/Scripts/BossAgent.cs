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
        private Transform aggro;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void InitializeAgent()
        {
            
            rBody = GetComponent<Rigidbody>();
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0f, 0.5f, 0f);
        }

        public Transform Aggro
        {
            get
            {
                return aggro;
            }
            set
            {
                aggro = value;
            }
        }

        public override void AgentReset()
        {
            health.baseValue = 100;
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0f, 0.5f, 0f);
        }

        public override float[] Heuristic()
        {
            float[] action =  { 0f, 0f };
            // For now we want the boss to simply turn toward
            // the player with its aggro
            //if (aggro != null)
            //{
            //    Vector3 aggroDir = aggro.position - transform.position;
            //    float step = rotateSpeed * Time.deltaTime;
            //    Vector3 newDir = Vector3.RotateTowards(transform.forward, aggroDir, step, 0.0f);
            //    Debug.DrawRay(transform.position, newDir, Color.red);
            //    //action[0] = newDir.normalized.y;
            //    //action[1] = 0;
            //    transform.rotation = Quaternion.LookRotation(newDir);
            //}
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
            //MoveActor(vectorAction[1]);
            //RotateActor(vectorAction[0]);

            // Fell off platform
            if (this.transform.position.y < 0)
            {
                Done();
            }
        }

        private Vector3 getPositionPleyerCentroid()
        {
            return Vector3.zero;
        }
    }
}

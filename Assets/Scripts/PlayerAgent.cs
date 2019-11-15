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
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void InitializeAgent()
        {
            rBody = GetComponent<Rigidbody>();
            boss = GameObject.FindGameObjectWithTag("Boss").transform;
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0f, 0.5f, -10f);
        }

        public override void AgentReset()
        {
            SpawnPoint point = raidArena.spawnManager.GetAvailableSpawn();
            if (point != null)
            {
                this.rBody.angularVelocity = Vector3.zero;
                this.rBody.velocity = Vector3.zero;
                this.transform.position = point.transform.position;
            }
            boss.GetComponent<Agent>().Done();
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
            if (distanceToTarget < 2.7f)
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
    }
}

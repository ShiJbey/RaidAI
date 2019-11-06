using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    public class BossAgent : Agent
    {
        Rigidbody rBody;

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
    }
}

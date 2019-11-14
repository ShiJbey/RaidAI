using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    public class AdController : MonoBehaviour
    {
        private Transform target;
        public float moveSpeed = 1.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //this.transform.position += new Vector3(0.0f, 0.0f, -0.1f);
            target = GameObject.FindWithTag("Player").transform;
            transform.LookAt(target);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
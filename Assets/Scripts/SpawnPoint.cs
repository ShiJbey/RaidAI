using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RaidAI
{
    public class SpawnPoint : MonoBehaviour
    {
        private bool available = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        public bool Available
        {
            get
            {
                return available;
            }
            set
            {
                available = value;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            available = false;
        }

        private void OnTriggerExit(Collider other)
        {
            available = true;
        }
    }
}

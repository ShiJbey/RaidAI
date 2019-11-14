using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    public class AdSpawner : MonoBehaviour
    {

        public GameObject adPrefab;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Spawn a new adversary when the user presses the space bar
            if (Input.GetKeyUp("space"))
            {
                Instantiate(adPrefab, transform.position, Quaternion.identity);
            }
        }

    }
}
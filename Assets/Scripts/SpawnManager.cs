using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace RaidAI
{
    // The Spawn manager class helps actors repsawn by returning
    // transforms to spawn points that it manages
    public class SpawnManager : MonoBehaviour
    {
        private SpawnPoint[] spawnPoints;

        // Start is called before the first frame update
        void Start()
        {
            spawnPoints = gameObject.GetComponentsInChildren<SpawnPoint>();
        }

        public SpawnPoint GetAvailableSpawn()
        {
            System.Random rnd = new System.Random();
            SpawnPoint[] shuffledPoints = spawnPoints.OrderBy(x => rnd.Next()).ToArray();
            for (int i = 0; i < shuffledPoints.Length; i++)
            {
                if (shuffledPoints[i].Available)
                {
                    return shuffledPoints[i];
                }
            }
            return null;
        }
    }
}
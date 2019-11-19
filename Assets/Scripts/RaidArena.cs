using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    // The arena is responsible for handing out team rewards
    // and reseting all the agents when the episode is complete
    public class RaidArena : MonoBehaviour
    {

        public SpawnManager spawnManager;
        private PlayerAgent[] players;
        public BossAgent boss;

        // Start is called before the first frame update
        void Start()
        {
            players = GetComponentsInChildren<PlayerAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsPartyAlive())
            {
                if (boss.IsAlive())
                {
                    boss.SetReward(1.0f);
                    RewardTeam(-1.0f);
                }
                else
                {
                    boss.SetReward(-1.0f);
                    RewardTeam(1.0f);
                }
                ResetArena();
            }
            else
            {
                if (!boss.IsAlive())
                {
                    boss.SetReward(-1.0f);
                    RewardTeam(2.0f);
                    ResetArena();
                }
            }
        }

        bool IsPartyAlive()
        {
            bool partyAlive = false;
            foreach (PlayerAgent player in players)
            {
                partyAlive = partyAlive || player.IsAlive();
            }
            return partyAlive;
        }

        void RewardTeam(float reward)
        {
            foreach (PlayerAgent player in players)
            {
                player.AddReward(reward);
            }
        }

        void ResetArena()
        {
            boss.Done();
            foreach(PlayerAgent player in players)
            {
                player.Done();
            }
        }
    }
}
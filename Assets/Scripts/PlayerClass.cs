using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [CreateAssetMenu (menuName = "Actor/PlayerClass")]
    public class PlayerClass : ScriptableObject
    {
        public CLASS_ID classID;
        public Color color;
        public ActorStat health;
        public ActorStat mana;
        public ActorStat energy;
        public ActorStat attack;
        public ActorStat defense;

        public enum CLASS_ID
        {
            Warrior,
            Mage,
            Tank,
            Healer
        }
    }
}

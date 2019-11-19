using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [CreateAssetMenu (menuName = "Actor/PlayerClass")]
    public class PlayerClass : ScriptableObject
    {
        public CLASS_ID classID = CLASS_ID.Warrior;
        public Color color = new Color(0.5f, 0.5f, 0.5f);
        public ActorStat health = new ActorStat(100f);
        public ActorStat mana = new ActorStat(100f);
        public ActorStat energy = new ActorStat(100f);
        public ActorStat attack = new ActorStat(10f);
        public ActorStat defense = new ActorStat(10f);

        public enum CLASS_ID
        {
            Warrior,
            Mage,
            Tank,
            Healer
        }
    }
}

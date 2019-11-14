using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [System.Serializable]
    public class Ability
    {
        public string name;
        public float amount;
        public float maxAmount;

        public AbilityType type;

        public Ability(string name, float amount, float maxAmount, AbilityType type)
        {
            this.name = name;
            this.amount = amount;
            this.maxAmount = maxAmount;
            this.type = type;
        }
    }

    public enum AbilityType
    {
        Stength,
        Speed,
        Magic
    }

}
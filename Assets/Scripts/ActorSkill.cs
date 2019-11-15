using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [CreateAssetMenu]
    public class ActorSkill : ScriptableObject
    {

        public string skillName;
        public Sprite icon;
        public string description;

        public int requiredLevel;
        public float requiredExp;

        public List<AbilityType> effectedTypes = new List<AbilityType>();
        public List<float> effectedAmmounts = new List<float>();

        public bool beenSelected;


        private float cost;
        private float cooldown;
        private float damage;

        public void Activate(Actor target)
        {

        }


    }

    public enum Element
    {
        NONE,
        FIRE,
        WATER,
        AIR,
        EARTH
    }

    public enum Range
    {
        SHORT,
        MID,
        LONG
    }

    public enum EffectType
    {
        DAMAGE,
        BUFF,
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    public abstract class ActorAbility : ScriptableObject
    {
        public string abilityName;
        public Sprite abilitySprite;
        public AudioClip soundClip;
        public float baseCoolDown = 1f;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
    }
}
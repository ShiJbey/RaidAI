using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    [CreateAssetMenu (menuName ="Abilities/AttackAbility")]
    public class RangedAttackAbility : ActorAbility
    {

        public float range = 3f;
        public float damage = 10f;
        
        public override void Initialize(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public override void TriggerAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}
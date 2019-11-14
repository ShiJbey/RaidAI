using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

namespace RaidAI
{   [Serializable]
    public class ActorStat
    {
        protected float baseValue;
        protected float lastBaseValue = float.MinValue;
        protected float value;
        protected bool isDirty;
        

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public ActorStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
            isDirty = true;
        }

        public ActorStat(float baseValue)
            : this()
        {
            this.baseValue = baseValue;
        }

        public virtual float Value
        {
            get
            {
                if (isDirty || lastBaseValue != baseValue)
                {
                    lastBaseValue = baseValue;
                    value = CalculateFinalValue();
                    isDirty = false;
                }
                return value;
            }
        }

        public virtual float BaseValue
        {
            get
            {
                return baseValue;
            }
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.type == StatModifier.StatModType.Flat)
                {
                    finalValue += mod.value;
                }
                else if (mod.type == StatModifier.StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModifier.StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.type == StatModifier.StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.order < b.order) return -1;
            if (a.order > b.order) return 1;
            return 0;
        }
    }
}
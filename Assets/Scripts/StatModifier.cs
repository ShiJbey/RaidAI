using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidAI
{
    public class StatModifier
    {
        public readonly float value;
        public readonly StatModType type;
        public readonly int order;
        public readonly object source;
        

        public StatModifier(float value, StatModType type, int order, object source)
        {
            this.value = value;
            this.type = type;
            this.order = order;
            this.source = source;
        }

        public StatModifier(float value, StatModType type)
            :this(value, type, (int)type, null)
        {

        }

        public StatModifier(float value, StatModType type, int order)
            : this(value, type, order, null)
        {

        }

        public StatModifier(float value, StatModType type, object source)
            : this(value, type, (int)type, source)
        {

        }

        public enum StatModType
        {
            Flat = 100,
            PercentAdd = 200,
            PercentMult = 300
        }
    }
}

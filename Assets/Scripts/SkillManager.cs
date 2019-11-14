using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace RaidAI
{
    public class SkillManager : MonoBehaviour
    {
        public List<ActorSkill> allSkills = new List<ActorSkill>();
        private List<SkillSlot> slots = new List<SkillSlot>();

        public Actor actor;

        public GameObject skillspacer;
        public GameObject skillslot;
    }
}
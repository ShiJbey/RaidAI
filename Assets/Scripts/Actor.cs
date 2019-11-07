using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
{
    private float baseLife;
    private float baseMana;
    private float baseAttack;
    private float baseDefense;

    private List<ActorSkill> skills;

    Actor(float baseLife, float baseMana, float baseAttack, float baseDefense)
    {
        this.baseLife = baseLife;
        this.baseMana = baseMana;
        this.baseAttack = baseAttack;
        this.baseDefense = baseDefense;
    }

    float[] GetStateVector()
    {
        return new float[4];
    }

}

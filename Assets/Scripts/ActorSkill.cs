using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSkill
{
    public enum Element
    {
        NONE,
        FIRE,
        WATER,
        AIR,
        EARTH
    };

    public enum Range
    {
        SHORT,
        MID,
        LONG
    };

    public enum EffectType
    {
        DAMAGE,
        BUFF,
    };

    private string name;
    private string description;

    private float cost;
    private float cooldown;
    private float damage;

    void ApplyTo(Actor target)
    {

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpellTarget
{
    Self,
    Allies,
    Enemies,
    MouseTarget,
    Barrier,
    World
}

public enum SpellEffect
{
    SlowMovement,
    SlowAttackSpeed,
    Heal,
    AddShield,
    GiveMana,
    SpawnBarrier,
    IncreaseManaRate,
    IncreaseAttackSpeed
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public SpellEffect spellEffect;
    public SpellTarget spellTarget;

    [Tooltip("Value that describes the ammount of what the spells do, For example: 100 in Heall All will heall all allies by 100")]
    public float spellAmmount;
    public float castTime = 0f;
    public float spellCost = 10f;

    [TextArea(3,10)]
    public string description;


    [Tooltip("Skill name in the UI")]
    public string spellName;
    [Tooltip("Skill image for the UI")]
    public Image spellImage;
}

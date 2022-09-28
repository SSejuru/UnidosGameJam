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
    IncreaseAttackSpeed,
    TOTALDESTRUCTION
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public SpellEffect spellEffect;
    public SpellTarget spellTarget;

    [Tooltip("Value that describes the ammount of what the spells do, For example: 100 in Heall will heall allies by 100")]
    public float spellAmmount;
    public float spellCost = 10f;
    public float cooldown = 1f;

    [TextArea(3,10)]
    public string description;

    [Tooltip("Skill name in the UI")]
    public string spellName;
    [Tooltip("Skill image for the UI")]
    public Sprite spellImage;
}

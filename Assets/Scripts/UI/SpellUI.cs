using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellUIStatus
{
    Avaliable,
    Cooldown,
    NeedMana
}

public class SpellUI : MonoBehaviour
{
    [SerializeField]
    private Spell _magicSpell;

    // Start is called before the first frame update
    void Start()
    {
        ManagerLocator.Instance._uiManager.AddSpell(this);

        // Set up Data
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSkillStatus(float playerMana)
    {

    }
}

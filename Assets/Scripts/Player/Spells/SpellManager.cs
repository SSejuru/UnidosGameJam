using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public void CastSpell(Spell spellToCast)
    {
        switch (spellToCast.spellTarget)
        {
            case SpellTarget.Self:
                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.IncreaseManaRate:
                        ManagerLocator.Instance._playerController.AddManaRegenRate(spellToCast.spellAmmount);
                        break;
                }
                break;
            case SpellTarget.Allies:
                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.IncreaseAttackSpeed:
                        break;
                    case SpellEffect.Heal:
                        break;                  
                    case SpellEffect.AddShield:
                        break;         
                    case SpellEffect.GiveMana:
                        break;      
                    case SpellEffect.IncreaseManaRate:
                        break;
                }
                break;
            case SpellTarget.Enemies:
                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.SlowMovement:
                        break;
                    case SpellEffect.SlowAttackSpeed:
                        break;                  
                }
                break;
            case SpellTarget.MouseTarget:

                //LivingBeing target = FindTarget();

                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.IncreaseAttackSpeed:
                        break;
                    case SpellEffect.Heal:
                        break;
                    case SpellEffect.AddShield:
                        break;
                    case SpellEffect.GiveMana:
                        break;
                    case SpellEffect.IncreaseManaRate:
                        break;
                }
                break;
            case SpellTarget.Barrier:
                switch (spellToCast.spellEffect)
                {              
                    case SpellEffect.Heal:
                        break;
                    case SpellEffect.AddShield:
                        break;
                }
                break;
            case SpellTarget.World:
                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.SpawnBarrier:
                        // Spawn barrier
                        break;
                }
                break;
        }
    }

}

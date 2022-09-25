using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public void CastSpell(Spell spellToCast, LivingBeing target = null)
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
                        ManagerLocator.Instance._npcManager.IncreaseAllAttackSpeed(spellToCast.spellAmmount);
                        break;
                    case SpellEffect.Heal:
                        ManagerLocator.Instance._npcManager.HealAllNPCS(spellToCast.spellAmmount);
                        break;                  
                    case SpellEffect.AddShield:
                        ManagerLocator.Instance._npcManager.ShieldAllNPCS(spellToCast.spellAmmount);
                        break;         
                    case SpellEffect.GiveMana:
                        ManagerLocator.Instance._npcManager.AddManaAllNPCS(spellToCast.spellAmmount);
                        break;      
                    case SpellEffect.IncreaseManaRate:
                        ManagerLocator.Instance._npcManager.IncreaseManaRateAll(spellToCast.spellAmmount);
                        break;
                }
                break;
            case SpellTarget.Enemies:
                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.SlowMovement:
                        ManagerLocator.Instance._enemiesManager.SlowEnemies();
                        break;
                    case SpellEffect.SlowAttackSpeed:
                        ManagerLocator.Instance._enemiesManager.ReduceEnemiesAttackSpeed();
                        break;
                    case SpellEffect.TOTALDESTRUCTION:
                        ManagerLocator.Instance._enemiesManager.DESTROYEVERYTHING();
                        break;
                }
                break;
            case SpellTarget.MouseTarget:

                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.IncreaseAttackSpeed:
                        target.IncreaseAttackSpeed(spellToCast.spellAmmount);
                        break;
                    case SpellEffect.Heal:
                        target.Heal(spellToCast.spellAmmount);
                        break;
                    case SpellEffect.AddShield:
                        target.GiveShield(spellToCast.spellAmmount);
                        break;
                    case SpellEffect.GiveMana:
                        target.AddMana(spellToCast.spellAmmount);
                        break;
                    case SpellEffect.IncreaseManaRate:
                        target.AddManaRegenRate(spellToCast.spellAmmount);
                        break;
                }
                break;
            case SpellTarget.Barrier:
                switch (spellToCast.spellEffect)
                {              
                    case SpellEffect.Heal:
                        //Heal Barrie
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

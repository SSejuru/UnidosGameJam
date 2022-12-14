using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    [SerializeField] private List<BattleNPC> _activeNPCs = new List<BattleNPC>();

    public List<BattleNPC> ActiveNPCs { get => _activeNPCs; set => _activeNPCs = value; }

    public void AddNpc(BattleNPC npc)
    {
        _activeNPCs.Add(npc);
    }

    public void RemoveNPCFromList(BattleNPC npc)
    {
        _activeNPCs.Remove(npc);
        ManagerLocator.Instance._winConditionManager.ListenerNPCDeath();
    }

    public void HealAllNPCS(float heal)
    {
        for(int i = 0; i < _activeNPCs.Count; i++)
        {
            _activeNPCs[i].Heal(heal);
        }
    }

    public void ShieldAllNPCS(float shield)
    {
        for (int i = 0; i < _activeNPCs.Count; i++)
        {
            _activeNPCs[i].GiveShield(shield);
        }
    }

    public void IncreaseAllAttackSpeed(float attackspeed)
    {
        for (int i = 0; i < _activeNPCs.Count; i++)
        {
            _activeNPCs[i].IncreaseAttackSpeed(attackspeed);
        }
    }

    public void ToggleAllNPCSHealthBar(bool value)
    {
        for (int i = 0; i < _activeNPCs.Count; i++)
        {
            _activeNPCs[i].ToggleHealthBar(value);
        }
    }

    public void TurnOnAllNPCSSpriteRenderer()
    {
        for (int i = 0; i < _activeNPCs.Count; i++)
        {
            _activeNPCs[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    [SerializeField] private List<BattleNPC> _activeNPCs = new List<BattleNPC>();

    public void AddNpc(BattleNPC npc)
    {
        _activeNPCs.Add(npc);
    }

    public void RemoveNPCFromList(BattleNPC npc)
    {
        _activeNPCs.Remove(npc);
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

}

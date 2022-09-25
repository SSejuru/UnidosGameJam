using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    [SerializeField] private List<BattleNPC> _activeNPCs = new List<BattleNPC>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

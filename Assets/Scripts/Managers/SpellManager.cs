using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    private const float SHAKE_TIMER = 1f;
    private const float SHAKE_INTENSITY = 2f;

    private float _currentShakeTimer;

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
                        StartCoroutine(PlayCameraShake());
                        ManagerLocator.Instance._enemiesManager.DESTROYEVERYTHING();
                        break;
                }
                break;
            case SpellTarget.MouseTarget:

                switch (spellToCast.spellEffect)
                {
                    case SpellEffect.Heal:
                        target.Heal(spellToCast.spellAmmount);
                        break;
                    case SpellEffect.AddShield:
                        target.GiveShield(spellToCast.spellAmmount);
                        break;                 
                }
                break;
        }

        ManagerLocator.Instance._playerController.AddMana(-spellToCast.spellCost);

    }

    private IEnumerator PlayCameraShake()
    {
        CinemachineBasicMultiChannelPerlin cameraNoise = _playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cameraNoise.m_AmplitudeGain = SHAKE_INTENSITY;

        float timeRate = 1 / SHAKE_TIMER;
        float time = 0f;

        while (time <= 1)
        {
            time += Time.deltaTime * timeRate; 
            cameraNoise.m_AmplitudeGain = Mathf.Lerp(SHAKE_INTENSITY, 0, time);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameState : StateApp {
	#region STATIC_ENUM_CONSTANTS
    public enum WEAPON_TYPE
    {
        PULSE = 0,
        TRAP = 1,
        GRANADE = 2
    }
	#endregion
	
	#region FIELDS
    public static event Action onTrapActivate = delegate { };

    public StageController stageController;

    public Text countPlasma;
    public Text totalPulse;
    public Text totalGranade;
    public Text totalTrap;
    public Image healthFilled;
    public Text time;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
    void Update()
    {
        time.text = Util.MilisecondsInClockFormat(GameManager.Instance.TimeGame);
        countPlasma.text = GameManager.Instance.Plasma.ToString();
    }
	#endregion
	
	#region METHODS_CUSTOM
	public override void Activate (){
		base.Activate ();

        AudioManager.Instance.PlayMusic(AudioManager.MUSIC_GAME, true);

        stageController.StartStage();

        healthFilled.fillAmount = 1;
        GameManager.Instance.Character.GetComponent<Health>().onReciveDamage += CharacterReciveDamage;

        UpdateUI();
	}

    public override void Desactivate()
    {
        AudioManager.Instance.Clear();
        base.Desactivate();
    }

    public void UpdateUI()
    {
        countPlasma.text = UserManager.Instance.UserConfiguration.totalPlasma.ToString();
        totalTrap.text = UserManager.Instance.UserConfiguration.totalTraps.ToString();
        totalPulse.text = UserManager.Instance.UserConfiguration.totalPulses.ToString();
        totalGranade.text = UserManager.Instance.UserConfiguration.totalExplosiveBullet.ToString();
    }

    private void TrapAttack()
    {
        if (UserManager.Instance.UserConfiguration.totalTraps > 0)
        {
            onTrapActivate();
            UserManager.Instance.UserConfiguration.totalTraps--;
            UserManager.Instance.SaveUserConfiguration();
        }
    }

    private void PulseAttack()
    {
        if (UserManager.Instance.UserConfiguration.totalPulses > 0)
        {
            GameManager.Instance.Character.GetComponent<ArmoredTowerController>().ActivePulseWeapon();
            UserManager.Instance.UserConfiguration.totalPulses--;
            UserManager.Instance.SaveUserConfiguration();
        }
    }

    private void GranadeAttack()
    {
        if (UserManager.Instance.UserConfiguration.totalExplosiveBullet > 0)
        {
            GameManager.Instance.Character.GetComponent<ArmoredTowerController>().ActiveGranadeWeapon();
            UserManager.Instance.UserConfiguration.totalExplosiveBullet--;
            UserManager.Instance.SaveUserConfiguration();
        }
    }
	#endregion
	
	#region EVENTS
    public void CharacterReciveDamage(float percHealth)
    {
        healthFilled.fillAmount = percHealth;
    }

    public void OnPauseButtonAction()
    {
        //TODO: Create pause state
    }

    public void OnWeaponButtonAction(int weaponType)
    {
        WEAPON_TYPE weapon = (WEAPON_TYPE)weaponType;

        if (weapon == WEAPON_TYPE.GRANADE)
        {
            GranadeAttack();
        }
        else if (weapon == WEAPON_TYPE.PULSE)
        {
            PulseAttack();
        }
        else if (weapon == WEAPON_TYPE.TRAP)
        {
            TrapAttack();
        }

        UpdateUI();
    }
	#endregion
}

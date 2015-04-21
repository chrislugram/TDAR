using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeState : StateApp
{
    #region STATIC_ENUM_CONSTANTS
    public static readonly int PRICE_EXTRA_LIFE = 100;
    public static readonly int PRICE_EXTRA_SPEED = 100;
    public static readonly int PRICE_EXPLOSIVE_BULLET = 200;
    public static readonly int PRICE_TRAP = 200;
    public static readonly int PRICE_PULSE = 200;

    public enum UPGRADE_TYPE{
        HEALTH = 0,
        SPEED = 1,
        EXPLOSIVE_BULLET = 2,
        TRAPS = 3,
        PULSE = 4
    }
    #endregion

    #region FIELDS
    public ArmoredTowerUI   armoredUI;
    public Text             countPlasma;

    public Text             costLife;
    public Text             costSpeed;
    public Text             costPulse;
    public Text             costExplosiveBullet;
    public Text             costTrap;

    public Text             totalLife;
    public Text             totalSpeed;
    public Text             totalPulse;
    public Text             totalExplosiveBullet;
    public Text             totalTrap;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    #endregion

    #region METHODS_CUSTOM
    public override void Activate()
    {
        base.Activate();

        
        UpdateUI();
    }

    private void UpdateUI()
    {
        countPlasma.text = UserManager.Instance.UserConfiguration.totalPlasma.ToString();

        costTrap.text = PRICE_TRAP.ToString();
        costExplosiveBullet.text = PRICE_EXPLOSIVE_BULLET.ToString();
        costPulse.text = PRICE_PULSE.ToString();
        costLife.text = (PRICE_EXTRA_LIFE * (UserManager.Instance.UserConfiguration.lifeArmoredTower+1)).ToString();
        costSpeed.text = (PRICE_EXTRA_SPEED * (UserManager.Instance.UserConfiguration.speedArmoredTower+1)).ToString();

        totalTrap.text = UserManager.Instance.UserConfiguration.totalTraps.ToString();
        totalPulse.text = UserManager.Instance.UserConfiguration.totalPulses.ToString();
        totalExplosiveBullet.text = UserManager.Instance.UserConfiguration.totalExplosiveBullet.ToString();
        totalLife.text = ((UserManager.Instance.UserConfiguration.lifeArmoredTower + 1) * 100).ToString();
        totalSpeed.text = (UserManager.Instance.UserConfiguration.speedArmoredTower + 1).ToString();
    }

    private void BuyTrap()
    {
        if (UserManager.Instance.UserConfiguration.totalTraps < 99 &&
            UserManager.Instance.UserConfiguration.totalPlasma >= PRICE_TRAP)
        {
            UserManager.Instance.UserConfiguration.totalTraps++;
            UserManager.Instance.UserConfiguration.totalPlasma -= PRICE_TRAP;
        }
    }

    private void BuySpeed()
    {
        int limitPlasma = PRICE_EXTRA_SPEED * (UserManager.Instance.UserConfiguration.speedArmoredTower + 1);

        if (UserManager.Instance.UserConfiguration.speedArmoredTower < 3 &&
           UserManager.Instance.UserConfiguration.totalPlasma >= limitPlasma)
        {
            UserManager.Instance.UserConfiguration.speedArmoredTower++;
            UserManager.Instance.UserConfiguration.totalPlasma -= limitPlasma;
        }
    }

    private void BuyPulse()
    {
        if (UserManager.Instance.UserConfiguration.totalPulses < 99 &&
           UserManager.Instance.UserConfiguration.totalPlasma >= PRICE_PULSE)
        {
            UserManager.Instance.UserConfiguration.totalPulses++;
            UserManager.Instance.UserConfiguration.totalPlasma -= PRICE_PULSE;
        }
    }

    private void BuyHealth()
    {
        int limitPlasma = PRICE_EXTRA_LIFE * (UserManager.Instance.UserConfiguration.lifeArmoredTower+1);
        
        if (UserManager.Instance.UserConfiguration.lifeArmoredTower < 3 &&
           UserManager.Instance.UserConfiguration.totalPlasma >= limitPlasma)
        {
            UserManager.Instance.UserConfiguration.lifeArmoredTower++;
            UserManager.Instance.UserConfiguration.totalPlasma -= limitPlasma;
        }
    }

    private void BuyExplosiveBullet()
    {
        if (UserManager.Instance.UserConfiguration.totalExplosiveBullet < 99 &&
          UserManager.Instance.UserConfiguration.totalPlasma >= PRICE_EXPLOSIVE_BULLET)
        {
            UserManager.Instance.UserConfiguration.totalExplosiveBullet++;
            UserManager.Instance.UserConfiguration.totalPlasma -= PRICE_EXPLOSIVE_BULLET;
        }
    }
    #endregion

    #region EVENTS
    public void OnBuyButton(int typeUpgrade)
    {
        UPGRADE_TYPE upgrade = (UPGRADE_TYPE)typeUpgrade;
        
        if (upgrade == UPGRADE_TYPE.EXPLOSIVE_BULLET)
        {
            BuyExplosiveBullet();
        }
        else if (upgrade == UPGRADE_TYPE.HEALTH)
        {
            BuyHealth();
        }
        else if (upgrade == UPGRADE_TYPE.PULSE)
        {
            BuyPulse();
        }
        else if (upgrade == UPGRADE_TYPE.SPEED)
        {
            BuySpeed();
        }
        else if (upgrade == UPGRADE_TYPE.TRAPS)
        {
            BuyTrap();
        }

        UserManager.Instance.SaveUserConfiguration();
        UpdateUI();
        armoredUI.UpdateUI();

        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
    }

    public void OnBackButtonAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.MAIN_MENU);
    }
    #endregion
}

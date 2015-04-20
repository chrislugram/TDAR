using UnityEngine;
using System.Collections;

public class ArmoredTowerUI : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public GameObject[] lifes;
    public GameObject[] canons;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Awake()
    {
        UpdateUI();
    }
    #endregion

    #region METHODS_CUSTOM
    public void UpdateUI()
    {
        //Configure lifes
        int totalLifes = UserManager.Instance.UserConfiguration.lifeArmoredTower;
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(i <= totalLifes);
        }

        //Configure canons
        int canonIndex = UserManager.Instance.UserConfiguration.speedArmoredTower;
        for (int i = 0; i < canons.Length; i++)
        {
            canons[i].SetActive(i == canonIndex);
        }
    }
    #endregion
}

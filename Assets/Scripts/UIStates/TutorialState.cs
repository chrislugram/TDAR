using UnityEngine;
using System.Collections;

public class TutorialState : StateApp {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public GameObject[] tutorialStates;

    private int indexTutorial;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    #endregion

    #region METHODS_CUSTOM
    public override void Activate()
    {
        base.Activate();
        indexTutorial = 0;

        ShowTutorial();
    }

    private void ShowTutorial()
    {
        if (indexTutorial == tutorialStates.Length)
        {
            UserManager.Instance.UserConfiguration.tutorial = 1;
            UserManager.Instance.SaveUserConfiguration();
            rootApp.ChangeState(StateReferenceApp.TYPE_STATE.GAME);
        }
        else
        {
            if (indexTutorial > 0)
            {
                tutorialStates[indexTutorial - 1].SetActive(false);
            }

            tutorialStates[indexTutorial].SetActive(true);
            indexTutorial++;
        }
    }
    #endregion

    #region EVENTS
    public void OnTutorialButton()
    {
        ShowTutorial();
    }
    #endregion
}

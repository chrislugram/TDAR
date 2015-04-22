using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndState : StateApp {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Text	textWinFail;
	public Text textTime;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public override void Activate (){
		base.Activate ();

        string message = "";

        if (UserManager.Instance.UserConfiguration.bestTime < GameManager.Instance.TimeGame)
        {
            UserManager.Instance.UserConfiguration.bestTime = GameManager.Instance.TimeGame;
            message += LocalizationApp.TextApp("endgame_besttime");
        }

        if (GameManager.Instance.WinGame)
        {
            message += LocalizationApp.TextApp("endgame_win");
        }
        else
        {
            message += LocalizationApp.TextApp("endgame_fail");
        }

        textWinFail.text = message;
        textTime.text = Util.MilisecondsInClockFormat(GameManager.Instance.TimeGame);

        UserManager.Instance.UserConfiguration.totalPlasma += GameManager.Instance.Plasma;
        UserManager.Instance.SaveUserConfiguration();
	}
	#endregion
	
	#region EVENTS
    public void OnRetryButtonAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        AudioManager.Instance.Clear();
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.GAME, AppScenes.SCENE_GAME);
    }

	public void OnBackButtonAction(){
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
		rootApp.ChangeState (StateReferenceApp.TYPE_STATE.MAIN_MENU, AppScenes.SCENE_MAIN_MENU);
	}
	#endregion
}

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

        UserManager.Instance.UserConfiguration.totalSpiderKilled += GameManager.Instance.TotalSpiderKilled;
        UserManager.Instance.UserConfiguration.totalWaspKilled += GameManager.Instance.TotalWaspKilled;

        UserManager.Instance.UserConfiguration.totalPlasma += GameManager.Instance.Plasma;
        UserManager.Instance.SaveUserConfiguration();

        //Send to GooglePlay information
        Social.ReportScore(GameManager.Instance.TimeGame, AppGooglePlayIDs.RANKING_ID, (bool success) =>{});

        CheckAchievement();
	}

    public override void Desactivate()
    {
        base.Desactivate();
    }

    private void CheckAchievement()
    {
        //Achievement 1:00
        if (UserManager.Instance.UserConfiguration.bestTime > 60000)
        {
            Social.ReportProgress(AppGooglePlayIDs.TIME_MORE_1_ID, 100.0, null);
        }

        //Achievement 2:30
        if (UserManager.Instance.UserConfiguration.bestTime > 150000)
        {
            Social.ReportProgress(AppGooglePlayIDs.TIME_MORE_2_5_ID, 100.0, null);
        }

        //Achievement 4:00
        if (UserManager.Instance.UserConfiguration.bestTime > 240000)
        {
            Social.ReportProgress(AppGooglePlayIDs.TIME_MORE_4_ID, 100.0, null);
        }

        //Spiders
        if (UserManager.Instance.UserConfiguration.totalSpiderKilled > 100)
        {
            Social.ReportProgress(AppGooglePlayIDs.KILL_SPIDER, 100.0, null);
        }

        //Wasp
        if (UserManager.Instance.UserConfiguration.totalWaspKilled > 100)
        {
            Social.ReportProgress(AppGooglePlayIDs.KILL_WASP, 100.0, null);
        }
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
        AudioManager.Instance.Clear();
		rootApp.ChangeState (StateReferenceApp.TYPE_STATE.MAIN_MENU, AppScenes.SCENE_MAIN_MENU);
	}
	#endregion
}

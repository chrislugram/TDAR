using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MainMenuState : StateApp {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
    public GameObject buttonRanking;
    public GameObject buttonAchievements;

	private Game	newGame;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public override void Activate (){
		base.Activate ();

        AdMobManager.Instance.ShowBanner();

        newGame = new Game();

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
        //If user get inicialized, not ask again
        ((PlayGamesPlatform)Social.Active).Authenticate((bool success) => { }, true);

        if (Social.localUser.authenticated){
            buttonRanking.SetActive(true);
            buttonAchievements.SetActive(true);
        }else{
            Social.localUser.Authenticate((bool success) =>{
                if (success)
                {
                    buttonRanking.SetActive(true);
                    buttonAchievements.SetActive(true);
                }
                else
                {
                    buttonRanking.SetActive(false);
                    buttonAchievements.SetActive(false);
                }
            });
        }

        AudioManager.Instance.PlayMusic(AudioManager.MUSIC_MAIN_MENU, true);
	}

    public override void Desactivate()
    {
        base.Desactivate();
    }
	#endregion
	
	#region EVENTS
	public void OnPlayButtonAction(){
        AdMobManager.Instance.HideBanner();

        GameManager.Instance.SetGame(newGame);
        AudioManager.Instance.StopMusic(AudioManager.MUSIC_MAIN_MENU);
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        AudioManager.Instance.Clear();

        if (UserManager.Instance.UserConfiguration.tutorial == 0)
        {
            rootApp.ChangeState(StateReferenceApp.TYPE_STATE.TUTORIAL, AppScenes.SCENE_GAME);
        }
        else
        {
            rootApp.ChangeState(StateReferenceApp.TYPE_STATE.GAME, AppScenes.SCENE_GAME);
        }
	}

    public void OnUpgradeButtonAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.UPGRADE);
    }

    public void OnOptionsButtonAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.OPTIONS);
    }

    public void OnButtonRankingAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(AppGooglePlayIDs.RANKING_ID);
    }

    public void OnButtonAchievementAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
    }
	#endregion
}

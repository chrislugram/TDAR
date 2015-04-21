using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuState : StateApp {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	private Game	newGame;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public override void Activate (){
		base.Activate ();

        newGame = new Game();

        AudioManager.Instance.PlayMusic(AudioManager.MUSIC_MAIN_MENU, true);
	}
	#endregion
	
	#region EVENTS
	public void OnPlayButtonAction(){
        GameManager.Instance.SetGame(newGame);
        AudioManager.Instance.StopMusic(AudioManager.MUSIC_MAIN_MENU);
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
		rootApp.ChangeState (StateReferenceApp.TYPE_STATE.GAME, AppScenes.SCENE_GAME);
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
	#endregion
}

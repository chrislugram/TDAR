using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuState : StateApp {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Text		labelTotalEnemies;
	public Text		labelMaxSpeed;
	public Text		labelBestTime;

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
	}
	#endregion
	
	#region EVENTS
	public void OnPlayButtonAction(){
        GameManager.Instance.SetGame(newGame);
		rootApp.ChangeState (StateReferenceApp.TYPE_STATE.GAME, AppScenes.SCENE_GAME);
	}

    public void OnUpgradeButtonAction()
    {
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.UPGRADE);
    }

    public void OnOptionsButtonAction()
    {
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.OPTIONS);
    }
	#endregion
}

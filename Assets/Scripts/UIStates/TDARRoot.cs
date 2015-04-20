using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TDARRoot : RootApp {
	#region STATIC_ENUM_CONSTANTS
	#endregion
		
	#region FIELDS
	#endregion
		
	#region ACCESSORS
	#endregion
		
	#region METHODS_UNITY
	#endregion
		
	#region METHODS_CUSTOM
	protected override void InitRootApp (){
		//Inicializo Sistemas
		TaskManager.Init ();
        UserManager.Instance.Init();
		//PlayerPrefs.DeleteAll ();
			
		//Inicializamos los estados
		states = new Dictionary<StateReferenceApp.TYPE_STATE, string> ();
		popupStates = new Dictionary<StateReferenceApp.POPUP_TYPE_STATE, string> ();
			
		//Añadimos los estados
		states.Add (StateReferenceApp.TYPE_STATE.INIT_MENU, StateReferenceApp.INIT_MENU);
		states.Add (StateReferenceApp.TYPE_STATE.MAIN_MENU, StateReferenceApp.MAIN_MENU);
		states.Add (StateReferenceApp.TYPE_STATE.GAME, StateReferenceApp.GAME);
		states.Add (StateReferenceApp.TYPE_STATE.END, StateReferenceApp.END);
        states.Add(StateReferenceApp.TYPE_STATE.UPGRADE, StateReferenceApp.UPGRADE);
        states.Add(StateReferenceApp.TYPE_STATE.OPTIONS, StateReferenceApp.OPTIONS);
		
		//Añadimos los popup
		//popupStates.Add (StateReferenceApp.POPUP_TYPE_STATE.POPUP_ALERT, StateReferenceApp.POPUP_ALERT);
			
		//Cargo la primera escena
		ChangeState (currentTypeState, AppScenes.SCENE_MAIN_MENU);
	}
	#endregion
}

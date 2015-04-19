﻿using UnityEngine;
using System;
using System.Collections;

public class InputGame : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string	ROTATE_AXIS = "Horizontal";
	#endregion
	
	#region FIELDS
	public static event Action	onMoveToRight = delegate {};
	public static event Action	onMoveToLeft = delegate {};
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	public void Update(){
        DetectKeyBoardEvents();
	}
	#endregion
	
	#region METHODS_CUSTOM
	public static void DetectKeyBoardEvents(){
		//Detect rotation in Speederbike
		if (Input.GetKey (KeyCode.A)) {
			onMoveToLeft ();
		} else if (Input.GetKey (KeyCode.D)) {
			onMoveToRight();
		}

		//HACK:Detect init run of Speederbike
		if (Input.GetKeyDown (KeyCode.Space)) {
			//GameManager.StartGame();
		}
	}
	#endregion
	
	#region EVENTS
	#endregion
}
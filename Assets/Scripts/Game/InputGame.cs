using UnityEngine;
using System;
using System.Collections;

public class InputGame : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string	ROTATE_AXIS = "Horizontal";

    public enum INPUT_TYPE
    {
        KEYBOARD = 0,
        TOUCH = 1
    }
	#endregion
	
	#region FIELDS
	public static event Action	onMoveToRight = delegate {};
	public static event Action	onMoveToLeft = delegate {};
    public static event Action onNothing = delegate { };

    private INPUT_TYPE inputType;
    private bool leftTouch = false;
    private bool rightTouch = false;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
    void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        inputType = INPUT_TYPE.KEYBOARD;
#elif UNITY_ANDROID
        inputType = INPUT_TYPE.TOUCH;
#endif
    }

	void Update(){
        if (inputType == INPUT_TYPE.KEYBOARD){
            DetectKeyBoardEvents();
        }
        else if (inputType == INPUT_TYPE.TOUCH)
        {
            if (leftTouch)
            {
                onMoveToLeft();
            }
            else if (rightTouch)
            {
                onMoveToRight();
            }
        }
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void DetectKeyBoardEvents(){
		//Detect rotation in Speederbike
		if (Input.GetKey (KeyCode.A)) {
			InputGame.onMoveToLeft ();
		} 
        else if (Input.GetKey (KeyCode.D)) 
        {
			InputGame.onMoveToRight();
        }
        else
        {
            InputGame.onNothing();
        }
	}

    /*public static void DetectAndroidInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x < (Screen.width * 0.5f))
            {
                onMoveToLeft();
            }
            else if (Input.GetTouch(0).position.x >= (Screen.width * 0.5f))
            {
                onMoveToRight();
            }
        } 
        else
        {
            onNothing();
        }

    }*/
	#endregion
	
	#region EVENTS
    public void OnPointerDown()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x < (Screen.width * 0.5f))
            {
                leftTouch = true;
            }
            else if (Input.GetTouch(0).position.x >= (Screen.width * 0.5f))
            {
                rightTouch = true;
            }
        } 
    }

    public void OnPointerUp()
    {
        leftTouch = false;
        rightTouch = false;
        onNothing();
    }
	#endregion
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager {
	#region STATIC_ENUM_CONSTANTS
    public static readonly int      MAX_TIME_IN_GAME = 600000;
	public static readonly string	CHARACTER_GO_TAG = "Player";

	public enum GAME_MODE{
		NORMAL = 0,
		MODE_AR = 1
	}
	#endregion
	
	#region FIELDS
    public event Action onEndGame = delegate { };

	private Game			    currentGame;
	private GameStats		    currentGameStats;
	private GameObject		    characterGO;
    private GAME_MODE           gameMode;
	private bool			    pauseFlag;
    private bool                finishGameFlag;
    private bool                winFlag = false;
	private CoroutineTask	    timeTask;

    private static GameManager  instance = null;
	#endregion
	
	#region ACCESSORS
	public Game CurrentGame{
		get{
			if (currentGame == null){
				currentGame = new Game();
				currentGameStats = new GameStats();
				Console.Warning("GAME IN EDIT MODE...");
			}
			return currentGame;
		}
	}

    public bool IsPaused
    {
        get { return pauseFlag; }
    }

	public GameObject Character{
		get{ return characterGO; }
        set{ characterGO = value; }
	}

	public GAME_MODE	GameMode{
        get { return gameMode; }
        set { gameMode = value; }
	}

	public int	TimeGame{
		get{
            if (currentGameStats == null)
            {
                return 0;
            }
            else
            {
                return currentGameStats.timeGame;
            }
        }
	}

    public int Plasma
    {
        get { return currentGameStats.plasma; }
    }

	public bool	WinGame{
		get { return winFlag; }
	}

    public bool FinishedGame
    {
        get { return finishGameFlag; }
    }

    public static GameManager Instance{
        get{
            if (instance == null){
                instance = new GameManager();
            }

            return instance;
        }
    }
	#endregion

    #region METHODS_CONSTRUCTORS
    private GameManager(){}
    #endregion

    #region METHODS_CUSTOM
    public void SetGame(Game game){
		currentGame = game;
	}

	public void StartGame(){
        timeTask = TaskManager.Launch(TimeCounter());

        finishGameFlag = false;
        winFlag = false;
        pauseFlag = false;

        characterGO = GameObject.FindGameObjectWithTag(CHARACTER_GO_TAG);

        ResetStats();
	}

	public void ResetStats(){
		currentGameStats = new GameStats ();
	}

	public void EnemyDestroyed(){
		currentGameStats.totalEnemyDestroyed++;
        Debug.Log("Total enemigos destruidos: " + currentGameStats.totalEnemyDestroyed + ", tiempo: " + ((float)currentGameStats.timeGame / 1000));
	}

    public void AddPlasma(int plasma)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromMilliseconds ((double)currentGameStats.timeGame);
        currentGameStats.plasma += plasma * (timeSpan.Minutes+1);
    }

	public void Pause(){
		if (pauseFlag) {
			pauseFlag = false;
			Time.timeScale = 1;
		} else {
			pauseFlag = true;
			Time.timeScale = 0;
		}
	}

	public void GameWin(){
		Debug.Log("Gana el jugador");
		winFlag = true;
		timeTask.Stop ();
        finishGameFlag = true;

		RootApp.Instance.ChangeState (StateReferenceApp.TYPE_STATE.END);
	}

	public void GameFail(){
		if (!winFlag) {
			Debug.Log("Pierde el jugador");
			winFlag = false;
			timeTask.Stop ();
            finishGameFlag = true;

            onEndGame();

            AudioManager.Instance.StopFXSound(AudioManager.MOVE_TOWER);
			RootApp.Instance.ChangeState (StateReferenceApp.TYPE_STATE.END);
		}
	}

	private IEnumerator TimeCounter(){
		while (true) {
			yield return new WaitForSeconds(0.1f);

			currentGameStats.timeGame += 100;

            if (currentGameStats.timeGame >= MAX_TIME_IN_GAME)
            {
                GameWin();
            }
		}
	}
	#endregion
	
	#region METHODS_EVENT
	#endregion
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string[]	PATH_ENEMIES_PREFAB = {
                                                              "Game/Enemy/Enemy01",
                                                              "Game/Enemy/Enemy02",
                                                              "Game/Enemy/Enemy03"
                                                          };
	public static readonly string	PATH_CHARACTER_PREFAB = "Game/Character/TowerCharacter";

	public enum GAME_MODE{
		NORMAL = 0,
		MODE_AR = 1
	}
	#endregion
	
	#region FIELDS
	private Game			    currentGame;
	private GameStats		    currentGameStats;
	private GameObject[]	    enemiesPrefab;
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

	public GameObject Character{
		get{ return characterGO; }
	}

	public GAME_MODE	GameMode{
        get { return gameMode; }
        set { gameMode = value; }
	}

	public int	TimeGame{
		get{ return currentGameStats.timeGame; }
	}

	public bool	WinGame{
		get { return winFlag; }
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
		currentGameStats = new GameStats ();
	}

	public void StartGame(){
        timeTask = TaskManager.Launch(TimeCounter());
        finishGameFlag = false;
        winFlag = false;
        pauseFlag = false;
	}

	public void ResetStats(){
		currentGameStats = new GameStats ();
	}

    //TODO
	public GameObject CreateCharacter(){
		/*if (characterGO == null){
			GameObject characterPrefab = (GameObject)Resources.Load(PATH_CHARACTER_PREFAB);
			characterGO = (GameObject) GameObject.Instantiate(characterPrefab);
		}
		*/
		return characterGO;
	}

    //TODO
	public GameObject CreateEnemy(){
		/*if (enemyPrefab == null){
			enemyPrefab = (GameObject)Resources.Load(PATH_ENEMY_PREFAB);
		}

		GameObject enemyInstant = (GameObject) GameObject.Instantiate(enemyPrefab);
		enemyInstances.Add (enemyInstant.transform);
         * */
        return characterGO;
	}

	public void EnemyDestroyed(){
		currentGameStats.totalEnemyDestroyed++;

		/*if (currentGameStats.totalEnemyDestroyed == currentGame.totalEnemies) {
			GameWin();
		}*/
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

		RootApp.Instance.ChangeState (StateReferenceApp.TYPE_STATE.END);
	}

	public void GameFail(){
		if (!winFlag) {
			Debug.Log("Pierde el jugador");
			winFlag = false;
			timeTask.Stop ();
			
			RootApp.Instance.ChangeState (StateReferenceApp.TYPE_STATE.END);
		}
	}

	private IEnumerator TimeCounter(){
		while (true) {
			yield return new WaitForSeconds(0.1f);

			currentGameStats.timeGame += 100;
		}
	}
	#endregion
	
	#region METHODS_EVENT
	#endregion
}

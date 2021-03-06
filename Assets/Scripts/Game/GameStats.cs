﻿using UnityEngine;
using System.Collections;

public class GameStats {

    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public int timeGame;
    public int totalEnemyDestroyed;
    public int plasma;
    public int totalSpiderKilled;
    public int totalWaspKilled;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_CONSTRUCTOR
    public GameStats()
    {
        timeGame = 0;
        totalEnemyDestroyed = 0;
        plasma = 0;
        totalSpiderKilled = 0;
        totalWaspKilled = 0;
    }
    #endregion

    #region METHODS
    #endregion
}

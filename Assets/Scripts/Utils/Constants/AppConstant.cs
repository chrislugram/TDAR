using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//Flag for it is necesary create distintc kind of build
public static class AppDevelopFlag{
	public static readonly bool	DEVELOP = true;
}

public static class AppLayers{
	public static readonly LayerMask	LAYER_PC = (LayerMask)10;
	public static readonly LayerMask	LAYER_ENEMY = (LayerMask)8;
	public static readonly LayerMask	LAYER_ENEMY_BULLET = (LayerMask)9;
	public static readonly LayerMask	LAYER_PC_BULLET = (LayerMask)11;
	public static readonly LayerMask	LAYER_GROUND = (LayerMask)12;
}

public static class AppScenes{
	public static readonly string	SCENE_INIT = "Init";
	public static readonly string	SCENE_MAIN_MENU = "MainMenu";
	public static readonly string	SCENE_GAME = "Game";
}

public static class AppFiles{
	public static readonly string 	FILE_USER = Application.persistentDataPath+"/configuration.json";
	public static readonly string	RESOURCES_FILE_USER = "JSON/configuration";
}

public static class AppPlayerPrefKeys
{
    public static readonly string KEY_USER_MUSIC_VOLUME = "music";
    public static readonly string KEY_USER_SFX_VOLUME = "sfx";
}

public static class AppGooglePlayIDs
{
    public static readonly string RANKING_ID = "CgkI_PrPpuQWEAIQBg";
    public static readonly string TIME_MORE_1_ID = "CgkI_PrPpuQWEAIQAQ";
    public static readonly string TIME_MORE_2_5_ID = "CgkI_PrPpuQWEAIQAg";
    public static readonly string TIME_MORE_4_ID = "CgkI_PrPpuQWEAIQAw";
    public static readonly string KILL_SPIDER = "CgkI_PrPpuQWEAIQBA";
    public static readonly string KILL_WASP = "CgkI_PrPpuQWEAIQBQ";
    public static readonly string ADMOB_ID = "ca-app-pub-9916552028793372/3867173042";
    public static readonly string PHONE_TEST_ID = "FCC29BEB119793A3FAFE403A9CF44779";
}

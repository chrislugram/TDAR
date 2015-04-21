using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager
{
    #region STATIC_ENUM_CONSTANTS
    public static readonly float MULTIPLAY_MUSIC_VOLUME = 0.5f;
    public static readonly string PREFAB_AUDIO_SAMPLE = "Prefabs/AudioSampleGO";

    //Musics
    public static readonly string MUSIC_MAIN_MENU = "FXsounds/Music/MainMenu01";
    public static readonly string MUSIC_GAME = "FXsounds/Music/LoopGame02";

    //GenericAudios
    public static readonly string BUTTON = "FXsounds/UI/Button01";

    #endregion

    #region FIELDS
    private GameObject prefabAudioSample;

    private List<AudioSample> cacheMusic;
    private List<AudioSample> cacheFXSounds;
    private Dictionary<string, AudioClip> audioSampleLoaded;

    private float musicVolume;
    private float sfxVolume;

    private static AudioManager instance = null;
    #endregion

    #region ACCESSORS

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }

    public float SFXVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = value; }
    }

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }

            return instance;
        }
    }
    #endregion

    #region METHODS_CONSTRUCTORS
    private AudioManager(){}
    #endregion

    #region METHODS_CUSTOM
    public void Init()
    {
        //comprobacion de configuracion
        if (PlayerPrefs.HasKey(AppPlayerPrefKeys.KEY_USER_MUSIC_VOLUME))
        {
            musicVolume = PlayerPrefs.GetFloat(AppPlayerPrefKeys.KEY_USER_MUSIC_VOLUME);
        }
        else
        {
            musicVolume = 1f;
            PlayerPrefs.SetFloat(AppPlayerPrefKeys.KEY_USER_MUSIC_VOLUME, 1f);
        }

        if (PlayerPrefs.HasKey(AppPlayerPrefKeys.KEY_USER_SFX_VOLUME))
        {
            sfxVolume = PlayerPrefs.GetFloat(AppPlayerPrefKeys.KEY_USER_SFX_VOLUME);
        }
        else
        {
            sfxVolume = 1f;
            PlayerPrefs.SetFloat(AppPlayerPrefKeys.KEY_USER_SFX_VOLUME, 1f);
        }


        prefabAudioSample = (GameObject)Resources.Load(PREFAB_AUDIO_SAMPLE);

        cacheMusic = new List<AudioSample>();
        cacheFXSounds = new List<AudioSample>();
        audioSampleLoaded = new Dictionary<string, AudioClip>();
    }


    public void SetMusicVolume(float volume)
    {
        musicVolume = volume * MULTIPLAY_MUSIC_VOLUME;
        //Limpieza Auxiliar
        foreach (AudioSample entry in cacheMusic)
        {
            if (entry == null)
            {
                cacheMusic.Remove(entry);
                return;
            }
        }


        foreach (AudioSample music in cacheMusic)
        {
            music.GetComponent<AudioSource>().volume = volume * MULTIPLAY_MUSIC_VOLUME;
        }

        PlayerPrefs.SetFloat(AppPlayerPrefKeys.KEY_USER_MUSIC_VOLUME, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        //Debug.Log ("He Cambiado");

        //Limpieza Auxiliar
        foreach (AudioSample entry in cacheFXSounds)
        {
            if (entry == null)
            {
                Debug.Log("borro uno");
                cacheFXSounds.Remove(entry);
                return;
            }
        }


        foreach (AudioSample sfx in cacheFXSounds)
        {
            sfx.GetComponent<AudioSource>().volume = volume;
        }

        PlayerPrefs.SetFloat(AppPlayerPrefKeys.KEY_USER_SFX_VOLUME, volume);
    }

    public void PlayMusic(string name, bool loop = false)
    {

        Play(name, cacheMusic, AudioManager.Instance.MusicVolume * MULTIPLAY_MUSIC_VOLUME, loop);
    }

    public void PlayFXSound(string name, bool loop = false)
    {
        Play(name, cacheFXSounds, AudioManager.Instance.SFXVolume, loop);
    }

    public void StopMusic(string name)
    {
        foreach (AudioSample entry in cacheMusic)
        {
            if (entry.AudioClipResource == name)
            {
                entry.Stop();
                return;
            }
        }
    }

    public void Clear()
    {
        cacheMusic.Clear();
        cacheFXSounds.Clear();

        foreach (string skey in audioSampleLoaded.Keys)
        {
            Resources.UnloadAsset(audioSampleLoaded[skey]);
        }

        audioSampleLoaded.Clear();
    }

    private void Play(string name, List<AudioSample> cache, float volume, bool loop = false)
    {
        //BORRADO DE CACHES
        foreach (AudioSample entry in cache)
        {
            if (entry == null)
            {
                cache.Remove(entry);
                return;
            }
        }


        foreach (AudioSample entry in cacheMusic)
        {
            if (entry == null)
            {
                cacheMusic.Remove(entry);
                return;
            }
        }


        foreach (AudioSample entry in cacheFXSounds)
        {
            if (entry == null)
            {
                cacheFXSounds.Remove(entry);
                return;
            }
        }

        //FIN BORRADO DE CACHES
        bool found = false;
        for (int i = 0; i < cache.Count && !found; i++)
        {
            if ((cache[i].AudioClipResource == name) && !cache[i].IsPlaying) 
            {
                cache[i].Play(loop, volume);
                found = true;
            }
        }

        if (!found)
        {
            CreateAudioSample(name, cache, volume, loop);
        }
    }

    private void CreateAudioSample(string name, List<AudioSample> cache, float volume, bool loop = false)
    {
        GameObject audioSampleGO = (GameObject)GameObject.Instantiate(prefabAudioSample);
        AudioSample audioSample = audioSampleGO.GetComponent<AudioSample>();

        AudioClip audioClip = GetAudioClip(name);

        audioSampleGO.name = audioClip.name;

        audioSample.Configure(audioClip, volume, loop, name);

        cache.Add(audioSample);
    }

    private AudioClip GetAudioClip(string name)
    {
        if (!audioSampleLoaded.ContainsKey(name))
        {
            AudioClip audioClip = Resources.Load<AudioClip>(name);
            audioSampleLoaded.Add(name, audioClip);
        }

        return audioSampleLoaded[name];
    }
    #endregion
}
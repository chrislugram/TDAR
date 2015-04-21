using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioSample : MonoBehaviour
{
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    private AudioSource audioSource;
    private string audioClipResource;
    #endregion

    #region ACCESSORS
    public bool IsPlaying
    {
        get { return audioSource.isPlaying; }
    }

    public string AudioClipResource
    {
        get { return audioClipResource; }
        set { audioClipResource = value; }
    }
    #endregion

    #region METHODS_CUSTOM
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    #endregion

    #region METHODS_CUSTOM
    public void Configure(AudioClip audioClip, float volume, bool loop, string resource)
    {
        audioSource.clip = audioClip;
        audioSource.loop = loop;

        audioClipResource = resource;

        Play(volume);
    }

    public void Play(float volume)
    {
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Play(bool loop, float volume)
    {
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
    #endregion
}
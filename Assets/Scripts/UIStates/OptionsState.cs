using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsState : StateApp {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public Slider musicSlider;
    public Slider fxSlider;
    public Toggle toggleEnglish;
    public Toggle toggleSpanish;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    #endregion

    #region METHODS_CUSTOM
    public override void Activate()
    {
        base.Activate();
        
        musicSlider.value = AudioManager.Instance.MusicVolume;
        fxSlider.value = AudioManager.Instance.SFXVolume;

        if (LocalizationApp.languageApp == LocalizationApp.LANGUAGE.ENGLISH)
        {
            Debug.Log("Idioma seleccionado ingles");
            toggleSpanish.isOn = false;
            toggleEnglish.isOn = true;
        }
        else if (LocalizationApp.languageApp == LocalizationApp.LANGUAGE.SPANISH)
        {
            Debug.Log("Idioma seleccionado español");
            toggleSpanish.isOn = true;
            toggleEnglish.isOn = false;
        }
    }
    #endregion

    #region EVENTS
    public void OnBackButtonAction()
    {
        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
        rootApp.ChangeState(StateReferenceApp.TYPE_STATE.MAIN_MENU);
    }

    public void OnSliderMusicChange(Slider slider)
    {
        AudioManager.Instance.SetMusicVolume(slider.value);
    }

    public void OnSliderFXChange(Slider slider)
    {
        AudioManager.Instance.SetSFXVolume(slider.value);
    }

    public void OnToggleEnglishActive(bool value)
    {
        if (value)
        {
            LocalizationApp.ChangeLanguageAppTo(LocalizationApp.LANGUAGE.ENGLISH);
        }

        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
    }

    public void OnToggleSpanishActive(bool value)
    {
        if (value)
        {
            LocalizationApp.ChangeLanguageAppTo(LocalizationApp.LANGUAGE.SPANISH);
        }

        AudioManager.Instance.PlayFXSound(AudioManager.BUTTON);
    }
    #endregion
}

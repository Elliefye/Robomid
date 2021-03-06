﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject MainButtons;
    public GameObject OptionsMenu;

    [SerializeField]
    private Slider volumeSlider;
    public bool NewGame = false;

    public void PlayClicked()
    {
        if (!GlobalControl.Instance.SaveFileExists())
            NewGame = true;
        Destroy(GlobalControl.Instance);

        if (NewGame)
            SceneManager.LoadScene("Tutorial");
        else SceneManager.LoadScene("Demo");
    }

    public void OptionsClicked()
    {
        MainButtons.SetActive(false);
        OptionsMenu.SetActive(true);
        //set volume slider according to save file data
        volumeSlider.value = GlobalControl.Instance.SavedPlayerData.Volume;
    }

    public void CreditsClicked()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        GlobalControl.Instance.SavedPlayerData.Volume = volumeSlider.value;
    }

    public void BackClicked()
    {
        GlobalControl.Instance.SaveGame();
        OptionsMenu.SetActive(false);
        MainButtons.SetActive(true);
    }

    public void CreditsBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

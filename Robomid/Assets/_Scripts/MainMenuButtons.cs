using System.Collections;
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

    public void PlayClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsClicked()
    {
        MainButtons.SetActive(false);
        OptionsMenu.SetActive(true);
        //set volume slider according to save file data
        //volumeSlider.value = Game.current.Volume;
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
        //save value to save file
    }

    public void BackClicked()
    {
        //save game
        OptionsMenu.SetActive(false);
        MainButtons.SetActive(true);
    }
}

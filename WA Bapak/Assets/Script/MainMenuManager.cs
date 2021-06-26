using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static bool soundOn;

    private void Start()
    {
        soundOn = true;
    }

    public void SoundClickON()
    {
        if (soundOn)
        {
            soundOn = false;
            FindObjectOfType<AudioManager>().Mute("");
            FindObjectOfType<AudioManager>().Play("");
        }
    }

    public void SoundClickOFF()
    {
        if (!soundOn)
        {
            soundOn = true;
            FindObjectOfType<AudioManager>().UnMute("MainMenu");
        }
    }

    public void ButtonClick()
    {
        if (soundOn)
        {
            FindObjectOfType<AudioManager>().Play("Click");
        }
    }


    public void QuitButton()
    {
        print("Exit");
        Application.Quit(0);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void SoundManager()
    {

    }

}

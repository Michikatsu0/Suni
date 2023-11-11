using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AnimManager : MonoBehaviour
{
    [SerializeField] private float animShortSpan, animLongSpan;
    [SerializeField] private VideoPlayer[] videosPlayer;
    [SerializeField] private GameObject[] uiElemetsList;
    
    private void Start()
    {
        GetNextScene();
        PlayerPrefs.Save();
    }

    private void GetNextScene()
    {
        AudioManager.Instance.backgroundVolumen = 0f;
        AudioManager.Instance.musicVolumen = 0f;
        PlayerPrefs.SetInt("SetAudio", 0);
        if (PlayerPrefs.GetInt("NewUser") == 1)
        {
            videosPlayer[1].Play();
            videosPlayer[1].loopPointReached += EndReached;
            uiElemetsList[0].GetComponent<Button>().onClick.AddListener(HomeLoader);
        }
        else
        {
            videosPlayer[0].Play();
            videosPlayer[0].loopPointReached += EndReached;
            uiElemetsList[0].GetComponent<Button>().onClick.AddListener(LoginLoader);
        }
    }

    private void EndReached(VideoPlayer source)
    {

        AudioManager.Instance.backgroundVolumen = 1f;
        AudioManager.Instance.musicVolumen = 1f;
        if (PlayerPrefs.GetInt("NewUser") == 1)
        {
            PlayerPrefs.SetInt("SetAudio", 0);
            PlayerPrefs.Save();
            SceneManagement.Instance.ChangeScene((int)AppScene.HOME);
        }
        else
        {
            PlayerPrefs.SetInt("NewUser", 1);
            PlayerPrefs.SetInt("SetAudio", 0);
            PlayerPrefs.Save();
            SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);
        }
        
    }

    private void HomeLoader()
    {
        PlayerPrefs.SetInt("SetAudio", 0);
        PlayerPrefs.Save();
        SceneManagement.Instance.ChangeScene((int)AppScene.HOME);
    }

    private void LoginLoader()
    {
        PlayerPrefs.SetInt("SetAudio", 0);
        PlayerPrefs.SetInt("NewUser", 1);
        PlayerPrefs.Save();
        SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);

    }

    public void SkipAnimation()
    {
        AudioManager.Instance.musicVolumen = 1f;
        AudioManager.Instance.backgroundVolumen = 1f;
        PlayerPrefs.SetInt("SetAudio", 0);
        PlayerPrefs.SetInt("NewUser", 1);
        PlayerPrefs.Save();
    }
}

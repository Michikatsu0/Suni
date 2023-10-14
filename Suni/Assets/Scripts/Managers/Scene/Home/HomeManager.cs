using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private int setAudio;

    private void Awake()
    {
        setAudio = PlayerPrefs.GetInt("SetAudio");
    }

    void Start()
    {
        
        UIElementsManager.Instance.DisableUI();
        if (setAudio == 1)
        {
            PlayerPrefs.SetInt("SetAudio", 0);
            AudioManager.Instance.SetAudio();
        }
    }

}

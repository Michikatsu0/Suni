using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private int setAudio;
    private Animator animatorTutorial;
    private void Awake()
    {
        setAudio = PlayerPrefs.GetInt("SetAudio");
        animatorTutorial = GameObject.Find("Tutorial").GetComponent<Animator>();
    }

    void Start()
    {
        AudioManager.Instance.musicVolumen = 1f;
        UIElementsManager.Instance.DisableUI();
        if (setAudio == 1)
        {
            PlayerPrefs.SetInt("SetAudio", 0);
            AudioManager.Instance.SetAudio();
        }
    }


    public void TutorialButtonMeditation()
    {
        animatorTutorial.SetBool("Meditation",true);
    }
    
    public void TutorialButtonEnd()
    {
        animatorTutorial.SetBool("End",true);
    }
    
}

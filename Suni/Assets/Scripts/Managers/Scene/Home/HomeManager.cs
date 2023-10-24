using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private int setAudio, setTutorial;
    private Animator animatorTutorial;
    private void Awake()
    {
        setAudio = PlayerPrefs.GetInt("SetAudio");
        setTutorial = PlayerPrefs.GetInt("SetTutorial");
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

        if (setTutorial == 0)
        {
            animatorTutorial.SetBool("Start", true);
        }
        else
        {
            animatorTutorial.enabled = false;
        }
    }


    public void TutorialButtonMeditation()
    {
        animatorTutorial.SetBool("Meditation",true);
    }
    
    public void TutorialButtonEnd()
    {
        animatorTutorial.SetBool("End",true);
        PlayerPrefs.SetInt("SetTutorial", 1);
    }
    
    
    
}

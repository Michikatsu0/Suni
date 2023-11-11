using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class HomeManager : MonoBehaviour
{
    private int setAudio, setTutorial, setMeditation;
    private Animator animatorTutorial;
    [SerializeField] private VideoPlayer backgroundVideoPlayer;
    [SerializeField] private List<VideoClip> backgroundVideos = new List<VideoClip>();
    private void Awake()
    {
        setAudio = PlayerPrefs.GetInt("SetAudio");
        setTutorial = PlayerPrefs.GetInt("SetTutorial");
        setMeditation = PlayerPrefs.GetInt("SetMeditation");
        animatorTutorial = GameObject.Find("Tutorial").GetComponent<Animator>();
    
    }

    void Start()
    {
        AudioManager.Instance.musicVolumen = 1f;
        UIElementsManager.Instance.DisableUI();
        AudioManager.Instance.backgroundVolumen = 1f;
        if (setAudio == 0)
        {
            PlayerPrefs.SetInt("SetAudio", 1);
            AudioManager.Instance.SetAudio();
        }

        if (setMeditation != 0)
        {
            PlayerPrefs.SetInt("SetMeditation", 0);
            backgroundVideoPlayer.clip = backgroundVideos[1];
            backgroundVideoPlayer.loopPointReached += EndReached;
        }

        if (setTutorial == 0)
            animatorTutorial.SetBool("Start", true);
        
        else
            animatorTutorial.enabled = false;
        
    }

    private void EndReached(VideoPlayer source)
    {
        backgroundVideoPlayer.clip = backgroundVideos[0];
    }

    public void TutorialButtonMeditation()
    {
        animatorTutorial.SetBool("Meditation",true);
    }
    
    public void TutorialButtonEnd()
    {
        animatorTutorial.SetBool("End", true);
        PlayerPrefs.SetInt("SetTutorial", 1);
    }

}

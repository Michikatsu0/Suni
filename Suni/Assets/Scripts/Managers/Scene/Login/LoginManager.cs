using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private bool resetPlayerPrefs;

    private RectTransform rectTransform;
    private string currentUserID;
    private int newUser = 0, setAudio; // 0=Y | 1=N
    private Vector2 rectTransfromVector = new Vector2(0f, 100);

    void Awake()
    {
        rectTransform = GameObject.Find("InputField_Password").GetComponent<RectTransform>();

        if (resetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("NewUser", 0);
            PlayerPrefs.SetInt("Menu", 0);
            PlayerPrefs.SetInt("SetAudio", 0);
            PlayerPrefs.SetInt("SetNotification", 0);
            PlayerPrefs.SetInt("MuteVoice", 0);
            PlayerPrefs.SetInt("MuteMusic", 0);
            PlayerPrefs.SetInt("MuteBackground", 0);
            PlayerPrefs.SetInt("SetTutorial", 0);
        }
        else
        {

            setAudio = PlayerPrefs.GetInt("SetAudio");
            if (setAudio == 1)
            {
                PlayerPrefs.SetInt("SetAudio", 0);
                AudioManager.Instance.SetAudio();
            }
            newUser = PlayerPrefs.GetInt("NewUser");
            if (newUser == 0)
                SceneManager.LoadSceneAsync((int)AppScene.ANIM);
        }

    }
    private void Start()
    {
        AudioManager.Instance.musicVolumen = 1f;
    }

    public void RegisterButton()
    {
        rectTransform.anchoredPosition -= rectTransfromVector;
    }

    public void ReturnSceneButton()
    {
        rectTransform.anchoredPosition += rectTransfromVector;
    }
    public void SetTutorialOldUser()
    {
        PlayerPrefs.SetInt("SetTutorial", 0);
    }
    public void SetTutorialNewUser()
    {
        PlayerPrefs.SetInt("SetTutorial", 0);
    }
}

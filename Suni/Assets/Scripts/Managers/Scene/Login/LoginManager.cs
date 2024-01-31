using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    private RectTransform rectTransform;
    private string currentUserID;
    private int newUser = 0, setAudio; // 0=Y | 1=N
    private Vector2 rectTransfromVector = new Vector2(0f, 100);

    void Awake()
    {
        rectTransform = GameObject.Find("InputField_Password").GetComponent<RectTransform>();
        newUser = PlayerPrefs.GetInt("NewUser");

        if (newUser == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Menu", 0);
            PlayerPrefs.SetInt("SetAudio", 0);
            PlayerPrefs.SetInt("SetTutorial", 0);
            PlayerPrefs.SetInt("SetMeditation", 0);
            PlayerPrefs.SetInt("MuteNotification", 1);
            PlayerPrefs.SetInt("MuteVoice", 1);
            PlayerPrefs.SetInt("MuteMusic", 1);
            PlayerPrefs.SetInt("MuteNoise", 1);
            PlayerPrefs.SetInt("MuteUI", 1);
            PlayerPrefs.SetString("NotificationDateTime", "yyyy-MM-dd HH:mm:ss");
            PlayerPrefs.SetInt("NotificationFlag", 0);
        }

        setAudio = PlayerPrefs.GetInt("SetAudio");
        if (setAudio == 0)
        {
            PlayerPrefs.SetInt("SetAudio", 1);
            AudioManager.Instance.SetAudio();
        }


        if (newUser == 0)
            SceneManager.LoadSceneAsync((int)AppScene.ANIM);

        PlayerPrefs.Save();
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
        PlayerPrefs.Save();
    }
}

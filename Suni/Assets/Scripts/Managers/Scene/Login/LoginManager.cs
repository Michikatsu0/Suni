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
    private int newUser = 0; // 0=Y | 1=N
    private Vector2 rectTransfromVector = new Vector2(0f, 100);

    void Awake()
    {
        rectTransform = GameObject.Find("InputField_Password").GetComponent<RectTransform>();
        if (resetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("NewUser", 0);
        }
        else
        {
            newUser = PlayerPrefs.GetInt("NewUser");
            if (newUser == 0)
                SceneManager.LoadSceneAsync((int)AppScene.ANIM);
        }

    }

    public void RegisterButton()
    {
        rectTransform.anchoredPosition -= rectTransfromVector;
    }

    public void ReturnSceneButton()
    {
        rectTransform.anchoredPosition += rectTransfromVector;
    }

}

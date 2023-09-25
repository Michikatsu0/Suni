using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElementList;

    // Start is called before the first frame update
    void Awake()
    {
        uiElementList[0].SetActive(false);
    }

    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene((int)AppScene.LOGIN);
    }
}

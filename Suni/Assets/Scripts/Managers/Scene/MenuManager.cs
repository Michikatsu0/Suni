using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    private GameObject menu; 
    
    private void Awake()
    {
        Instance = this;
        menu = GameObject.Find("Menu");
        EnableMenu();
    }

    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene((int)AppScene.LOGIN);
    }

    public void SetMenu()
    {
        if (menu.activeSelf)
            PlayerPrefs.SetInt("Menu", 1);
        else
            PlayerPrefs.SetInt("Menu", 0);

        EnableMenu();
    }

    public void EnableMenu(bool setActive)
    {
        menu.SetActive(setActive);
        if (setActive)
            PlayerPrefs.SetInt("Menu", 1);
        else
            PlayerPrefs.SetInt("Menu", 0);
    }

    public void EnableMenu()
    {
        if (PlayerPrefs.GetInt("Menu") == 1)
            menu.SetActive(true);
        else
            menu.SetActive(false);
    }
}

using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElementsManager : MonoBehaviour
{
    public static UIElementsManager Instance;
    [SerializeField] private GameObject[] uIElementsList;

    private void Awake()
    {
        Instance = this;
    }

    public void DisableUI()
    {
        foreach (var element in uIElementsList) {
            element.SetActive(false);
        }
    }

    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene((int)AppScene.LOGIN);
    }
}

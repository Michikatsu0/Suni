using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        UIElementsManager.Instance.DisableUI();
    }

}

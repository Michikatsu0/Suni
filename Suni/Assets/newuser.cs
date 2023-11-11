using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newuser : MonoBehaviour
{
    public bool resetNewUser;
    void Awake()
    {
        if (resetNewUser)
            PlayerPrefs.SetInt("NewUser", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElemetsList;

    // Start is called before the first frame update
    void Awake()
    {
        uiElemetsList[0].SetActive(false);    
    }
}

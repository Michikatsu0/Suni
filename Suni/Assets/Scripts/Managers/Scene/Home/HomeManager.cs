using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElementList;

    // Start is called before the first frame update
    void Awake()
    {
        uiElementList[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

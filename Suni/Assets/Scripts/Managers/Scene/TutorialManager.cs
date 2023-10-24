using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int setTutorial;
    [SerializeField] private List<GameObject> uiTutorial = new List<GameObject>();
    void Start()
    {
        setTutorial = PlayerPrefs.GetInt("SetTutorial");
        if (setTutorial == 0)
        {
            SetUITutorial(true);
        }
        else
        {
            SetUITutorial(false);
        }
    }

    private void SetUITutorial(bool enable)
    {
        foreach(var ui in uiTutorial)
            ui.SetActive(enable);
    }
}

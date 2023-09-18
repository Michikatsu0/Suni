using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewWindow : MonoBehaviour
{
    public GameObject window;

    public void Show()
    {
        if (window != null)
        {
            window.SetActive(true);
        }
    }
    public void Hide()
    {
        if (window != null)
        {
            window.SetActive(false);
        }
    }
}

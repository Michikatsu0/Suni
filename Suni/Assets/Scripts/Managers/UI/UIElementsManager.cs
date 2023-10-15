using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIElementsManager : MonoBehaviour
{
    public static UIElementsManager Instance;
    [SerializeField] private GameObject[] uIElementsList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.musicVolumen = 1;
        AudioManager.Instance.GetSettings();
    }

    public void DisableUI()
    {
        foreach (var element in uIElementsList) {
            element.SetActive(false);
        }
    }

    public void PlaySound(int audioClip)
    {
        AudioManager.Instance.PlaySound(audioClip);
    }
}

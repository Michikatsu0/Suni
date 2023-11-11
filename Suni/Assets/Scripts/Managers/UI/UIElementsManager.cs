using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIElementsManager : MonoBehaviour
{
    public static UIElementsManager Instance;
    [SerializeField] private GameObject[] uIElementsList;
    public DateTime scheduledTime;
    public bool flag ,internalflag;
    [SerializeField] private GameObject uINotification;
    private FirebaseAuth auth;
    private void Awake()
    {
        Instance = this;
        auth = FirebaseAuth.DefaultInstance;
        if (DateTime.TryParseExact(PlayerPrefs.GetString("NotificationDateTime"), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.DefaultThreadCurrentCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
        {
            // La conversión fue exitosa, 'result' ahora contiene el objeto DateTime
            Console.WriteLine("DateTime: " + result);
            scheduledTime = result;
        }

        if (PlayerPrefs.GetInt("NotificationFlag") == 0)
            flag = false;
        else
            flag = true;
    }

    private void Start()
    {
        DisableUI();
        AudioManager.Instance.musicVolumen = 1;
        AudioManager.Instance.GetSettings();
    }

    private void Update()
    {
        if (!uINotification) return;
        if (auth.CurrentUser == null) return;
        if (scheduledTime.Year <= 2022) return;
        if (DateTime.Now >= scheduledTime && flag)
        {
            if (SceneManager.GetActiveScene().buildIndex != (int)AppScene.LOGIN || SceneManager.GetActiveScene().buildIndex != (int)AppScene.REGISTER || SceneManager.GetActiveScene().buildIndex != (int)AppScene.ANIM)
                ShowUINotificationInApp();
            flag = false;
        }
    }

    private void ShowUINotificationInApp()
    {
        uINotification.SetActive(true);
        scheduledTime.AddDays(1);
        PlaySoundNotification(0);
        string dateTimeString = scheduledTime.ToString("yyyy-MM-dd HH:mm:ss");
        PlayerPrefs.SetString("NotificationDateTime", dateTimeString);
        PlayerPrefs.SetInt("NotificationFlag", 0);
        Debug.Log(scheduledTime);
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
    public void PlaySoundVoice(int audioClip)
    {
        AudioManager.Instance.PlaySoundVoice(audioClip);
    }
    public void PlaySoundNotification(int audioClip)
    {
        AudioManager.Instance.PlaySoundNotification(audioClip);
    }
    public void PlaySoundBackground(int audioClip)
    {
        AudioManager.Instance.PlaySoundBackground(audioClip);
    }
    private void OnApplicationQuit()
    {
        // Guarda aquí tus datos de PlayerPrefs antes de cerrar la aplicación
        PlayerPrefs.Save();
    }
}

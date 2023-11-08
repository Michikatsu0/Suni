using Firebase;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class RegisterManager : MonoBehaviour
{
    public static RegisterManager Instance;

    [SerializeField] private List<TMP_Dropdown> dropdownsNotf = new List<TMP_Dropdown>();
    [SerializeField] private List<string> dropdownStringsHours = new List<string>();
    [SerializeField] private List<string> dropdownStringsMinutes = new List<string>();
    [SerializeField] private List<string> dropdownStringsDayRef = new List<string>();

    [SerializeField] private Button scheduleButton;

    private FirebaseMessaging messaging;
    private DateTime scheduledTime;
    private FirebaseApp app;

    private void Awake()
    {
        dropdownsNotf[0].ClearOptions();
        dropdownsNotf[0].AddOptions(dropdownStringsHours);

        dropdownsNotf[1].ClearOptions();
        dropdownsNotf[1].AddOptions(dropdownStringsMinutes);

        dropdownsNotf[2].ClearOptions();
        dropdownsNotf[2].AddOptions(dropdownStringsDayRef);
    }

    public void Start()
    {
        UIElementsManager.Instance.DisableUI();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        FirebaseMessaging.TokenReceived += OnTokenReceived;
        FirebaseMessaging.MessageReceived += OnMessageReceived;
        scheduleButton.onClick.AddListener(ScheduleNotification);
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }

    void ScheduleNotification()
    {
        int hour = int.Parse(dropdownsNotf[0].options[dropdownsNotf[0].value].text);
        int minute = int.Parse(dropdownsNotf[1].options[dropdownsNotf[1].value].text);
        string amPm = dropdownsNotf[2].options[dropdownsNotf[2].value].text;

        if (amPm == "P.M." && hour < 12)
        {
            hour += 12;
        }
        else if (amPm == "A.M." && hour == 12)
        {
            hour = 0;
        }

        scheduledTime = DateTime.Now.Date.AddHours(hour).AddMinutes(minute);

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


    }
}
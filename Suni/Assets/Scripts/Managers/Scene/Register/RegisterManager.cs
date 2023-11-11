using Firebase;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Unity.VisualScripting;

public class RegisterManager : MonoBehaviour
{
    public static RegisterManager Instance;

    [SerializeField] private List<TMP_Dropdown> dropdownsNotf = new List<TMP_Dropdown>();
    [SerializeField] private List<string> dropdownStringsHours = new List<string>();
    [SerializeField] private List<string> dropdownStringsMinutes = new List<string>();
    [SerializeField] private List<string> dropdownStringsDayRef = new List<string>();

    [SerializeField] private Button scheduleButton;

    private DateTime scheduledTime;
    private FirebaseAuth auth;
    private string username;


    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        GetUsername(auth);

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
        scheduleButton.onClick.AddListener(ScheduleNotification);
    }

    private void GetUsername(FirebaseAuth auth)
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("users/" + auth.CurrentUser.UserId + "/username")
        .GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
                Debug.Log(task.Exception);
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                username = "" + snapshot.Value;

                Debug.LogFormat(
                "User signed in successfully: {0} ({1} : {2})",
                snapshot.Value,
                auth.CurrentUser.Email,
                auth.CurrentUser.UserId);
            }
        });
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
            hour += 12;
        else if (amPm == "A.M." && hour == 12)
            hour = 0;

        scheduledTime = DateTime.Now.Date.AddHours(hour).AddMinutes(minute);

        string dateTimeString = scheduledTime.ToString("yyyy-MM-dd HH:mm:ss");
        PlayerPrefs.SetString("NotificationDateTime", dateTimeString);
        PlayerPrefs.SetInt("NotificationFlag", 1);

        PlayerPrefs.Save();
        Debug.Log(scheduledTime);
    }
}
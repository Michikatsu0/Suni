using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ExperienceBarController : MonoBehaviour
{
    DatabaseReference mDatabase;
    string UserId;

    public Slider experienceBar;
    public float maxExperience;
    [SerializeField] private float currentExperience;

    [SerializeField] private float experienceGained = 100f;
    [SerializeField] private int level;

    public TMP_Text levelTex;
    public TMP_Text experienceGainedTex;

    public Material newFillMaterial;

    public VideoPlayer videoPlayer;

    private bool isVideoPlaying = false;
    [SerializeField] private float experienceInterval = 2f;

    private void Start()
    {
        GetUserStats();
        
        experienceBar.fillRect.GetComponent<Image>().material = newFillMaterial;

        videoPlayer.started += OnVideoStarted;
        videoPlayer.loopPointReached += OnVideoLoopPointReached;

        StartCoroutine(GainExperiencePeriodically());
    }

    private void GetUserStats()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + UserId + "/currentExperience")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string _maxExperience = "" + snapshot.Value;
                    maxExperience = float.Parse(_maxExperience);
                }
            });
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + UserId + "/maxExperience")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string _currentExperience = "" + snapshot.Value;
                    currentExperience = float.Parse(_currentExperience);
                }
            });
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + UserId + "/level")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string _level = "" + snapshot.Value;
                    level = int.Parse(_level);
                }
            });
    }

    private void OnVideoStarted(VideoPlayer vp)
    {
        isVideoPlaying = true;
    }

    private void OnVideoLoopPointReached(VideoPlayer vp)
    {
        isVideoPlaying = false;
    }

    private IEnumerator GainExperiencePeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(experienceInterval);

            if (isVideoPlaying)
            {
                GainExperience();
            }
        }
    }

    public void GainExperience()
    {
        currentExperience += experienceGained;
        if (currentExperience >= maxExperience)
        {
            level++;
            maxExperience =+ 500;
            SetNewLevel();
            currentExperience = 0;
        }
        UpdateExperienceBar();
        StartCoroutine(SeeExperience());
    }
    private void SetNewLevel()
    {
        levelTex.text = ("Level " + level);
        mDatabase.Child("users").Child(UserId).Child("level").SetValueAsync(level);
        mDatabase.Child("users").Child(UserId).Child("currentExperience").SetValueAsync(0);
        mDatabase.Child("users").Child(UserId).Child("maxExperience").SetValueAsync(maxExperience);
        currentExperience = 0;
    }

    private IEnumerator SeeExperience()
    {
        experienceGainedTex.text = ("+" + experienceGained);
        yield return new WaitForSeconds(2f);
        experienceGainedTex.text = ("");
    }

    private void UpdateExperienceBar()
    {
        float fillAmount = currentExperience;
        experienceBar.value = fillAmount;
    }
}

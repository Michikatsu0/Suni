using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ExperienceBarController : MonoBehaviour
{
    public static ExperienceBarController Instance;
    DatabaseReference mDatabase;
    FirebaseAuth mAuth;

    public Slider experienceBar;
    
    [SerializeField] private int level = 1;
    [SerializeField] private float currentXP;
    [SerializeField] private float requiredXP;
    [SerializeField] private float a = 4.5f, b = 1.5f, c = 1f;
    [SerializeField] [Range(1f, 300f)] private float additionMultiplier = 300;
    [SerializeField] [Range(2f,4f)] private float powerMultiplier = 2;
    [SerializeField] [Range(7f,14f)] private float divisionMultiplier = 7;
    

    public TMP_Text levelText;
    public TMP_Text currentTextXP;
    public TMP_Text requiredTextXP;

    public VideoPlayer videoPlayer;

    private bool isVideoPlaying = false;
    [SerializeField] private float experienceInterval = 3f;

    private void Awake()
    {
        Instance = this;
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
        mAuth = FirebaseAuth.DefaultInstance;

        
        GetUserStats(mAuth);
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)AppScene.HOME)
        {
            videoPlayer.started += OnVideoStarted;
            videoPlayer.loopPointReached += OnVideoLoopPointReached;

            StartCoroutine(GainExperiencePeriodically());
        }
    }

    public void GetUserStats(FirebaseAuth auth)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + auth.CurrentUser.UserId + "/level")
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
                    levelText.text = "Level: " + level.ToString();
                    requiredXP = CalculateRequiredXP();
                }
            });
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + auth.CurrentUser.UserId + "/currentXP")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string _currentXP = "" + snapshot.Value;
                    currentXP = float.Parse(_currentXP);
                    currentTextXP.text = currentXP.ToString();
                    experienceBar.value = currentXP;
                }
            });
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + auth.CurrentUser.UserId + "/requiredXP")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string _requiredXP = "" + snapshot.Value;
                    requiredXP = float.Parse(_requiredXP);
                    requiredTextXP.text = requiredXP.ToString();
                    experienceBar.maxValue = requiredXP;
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
                GainXP(CalculateGetXP());
            }
        }
    }

    public int CalculateGetXP()
    {
        float xp = a * level + b * Mathf.Log(level + c);
        return Mathf.RoundToInt(xp);
    }

    private int CalculateRequiredXP()
    {
        int requiredXP = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
            requiredXP += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));           
        
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("requiredXP").SetValueAsync(requiredXP / 4);
        return requiredXP/4;
    }

    public void GainXP(float gainedXP)
    {
        currentXP += gainedXP;
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("currentXP").SetValueAsync(currentXP);
        if (currentXP > requiredXP)
        {
            requiredXP = CalculateRequiredXP();
            SetNewLevel();
        }
        UpdateExperienceBar();
        StartCoroutine(SeeExperience());
    }

    private void SetNewLevel()
    {
        level++;
        currentXP = 0;
        levelText.text = ("Level " + level);
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("level").SetValueAsync(level);
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("currentXP").SetValueAsync(currentXP);
        requiredXP = CalculateRequiredXP();
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("requiredXP").SetValueAsync(requiredXP);
    }

    private IEnumerator SeeExperience()
    {
        currentTextXP.text = currentXP.ToString();
        requiredTextXP.text = requiredXP.ToString();
        yield return null;
    }

    private void UpdateExperienceBar()
    {
        experienceBar.value = currentXP;
        experienceBar.maxValue = requiredXP;
    }
}

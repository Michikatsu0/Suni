using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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

    [SerializeField] private float experienceInterval = 5f;

    private void Awake()
    {
        Instance = this;
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
        mAuth = FirebaseAuth.DefaultInstance;
        if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.MEDITATION || SceneManager.GetActiveScene().buildIndex == (int)AppScene.HOME)
            GetUserStats(mAuth);
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)AppScene.MEDITATION)
            StartCoroutine(GainExperiencePeriodically());
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
                    requiredXP = CalculateRequiredXp();
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
                    string currentXp = "" + snapshot.Value;
                    currentXP = float.Parse(currentXp);
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
                    string requiredXp = "" + snapshot.Value;
                    this.requiredXP = float.Parse(requiredXp);
                    requiredTextXP.text = requiredXP.ToString();
                    experienceBar.maxValue = requiredXP;
                }
            });
    }

    private IEnumerator GainExperiencePeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(experienceInterval);

            if (MeditationManager.Instance.isPlayingSuniMed)
            {
                GainXp(CalculateGetXp());
            }
        }
    }

    public int CalculateGetXp()
    {
        var xp = a * level + b * Mathf.Log(level + c);
        return Mathf.RoundToInt(xp);
    }

    public int CalculateRequiredXp()
    {
        var requiredXp = 0;
        for (var levelCycle = 1; levelCycle <= level; levelCycle++)
            requiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));           
        
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("requiredXP").SetValueAsync(requiredXp / 4);
        return requiredXp/4;
    }

    public void GainXp(float gainedXp)
    {
        currentXP += gainedXp;
        mDatabase.Child("users").Child(mAuth.CurrentUser.UserId).Child("currentXP").SetValueAsync(currentXP);
        if (currentXP > requiredXP)
        {
            requiredXP = CalculateRequiredXp();
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
        requiredXP = CalculateRequiredXp();
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimManager : MonoBehaviour
{
    [SerializeField] private float animShortSpan, animLongSpan;
    [SerializeField] private GameObject[] uiElemetsList;
    private TMP_Text countLabel;
    private float time, animSpan;

    private void Awake()
    {
        GetNextScene();

        countLabel = GameObject.Find("Label_Count").GetComponent<TMP_Text>();
        time = animSpan + 1f;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time >= 0.0f)
        {
            var a = (int)time;
            countLabel.text = a.ToString();
        }
        else
        {
            if (PlayerPrefs.GetInt("NewUser") == 1)
                SceneManagement.Instance.ChangeScene((int)AppScene.HOME);
            else
            {
                PlayerPrefs.SetInt("NewUser", 1);
                SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);
            }
        }
    }

    private void GetNextScene()
    {
        if (PlayerPrefs.GetInt("NewUser") == 1)
        {
            animSpan = animLongSpan;
            uiElemetsList[0].GetComponent<Button>().onClick.AddListener(HomeLoader);
        }
        else
        {
            animSpan = animShortSpan;
            uiElemetsList[0].GetComponent<Button>().onClick.AddListener(LoginLoader);
        }
    }

    private void HomeLoader()
    {
        SceneManagement.Instance.ChangeScene((int)AppScene.HOME);
    }

    private void LoginLoader()
    {
        SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);
    }

    public void SkipAnimation()
    {
        PlayerPrefs.SetInt("NewUser", 1);
    }
}

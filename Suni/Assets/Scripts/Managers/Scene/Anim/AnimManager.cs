using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimManager : MonoBehaviour
{
    public float animShortSpan, animLongSpan;
    private TMP_Text countLabel;
    private float time;
    private float animSpan;
    private void Start()
    {
        if (PlayerPrefs.GetInt("NewUser") == 1)
            animSpan = animLongSpan;
        else
            animSpan = animShortSpan;

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

    public void SkipAnimation()
    {
        PlayerPrefs.SetInt("NewUser", 1);
        SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);
    }
}

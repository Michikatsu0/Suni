using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimManager : MonoBehaviour
{
    private TMP_Text countLabel;
    private float time;
    public float animSpan;
    private void Start()
    {
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
            PlayerPrefs.SetInt("NewUser", 1);
            SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);
        }
    }

    public void SkipAnimation()
    {
        PlayerPrefs.SetInt("NewUser", 1);
        SceneManagement.Instance.ChangeScene((int)AppScene.LOGIN);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Texture firstTexture, secondTexture;
    [SerializeField] private RawImage[] rawImages;

    private void Start()
    {
        UIElementsManager.Instance.DisableUI();
        AudioManager.Instance.GetSettings();
        SetSettings();
    }

    public void SetSettings()
    {
        if (AudioManager.Instance.notification != 0)
            rawImages[0].texture = secondTexture;
        else
            rawImages[0].texture = firstTexture;

        if (AudioManager.Instance.voice != 0)
            rawImages[1].texture = secondTexture;
        else
            rawImages[1].texture = firstTexture;

        if (AudioManager.Instance.music != 0)
            rawImages[2].texture = secondTexture;
        else
            rawImages[2].texture = firstTexture;

        if (AudioManager.Instance.background != 0)
            rawImages[3].texture = secondTexture;
        else
            rawImages[3].texture = firstTexture;

        if (AudioManager.Instance.ui != 0)
            rawImages[4].texture = secondTexture;
        else
            rawImages[4].texture = firstTexture;
    }

    public void ChangeTexture(int elementIndex)
    {
        if (elementIndex >= 0 && elementIndex < rawImages.Length)
        {
            if (elementIndex == 0)
            {
                if (rawImages[elementIndex].texture == firstTexture)
                    PlayerPrefs.SetInt("SetNotification", 1);
                else
                    PlayerPrefs.SetInt("SetNotification", 0);
            }
            else if (elementIndex == 1)
            {
                if (rawImages[elementIndex].texture == firstTexture)
                    PlayerPrefs.SetInt("MuteVoice", 1);
                else
                    PlayerPrefs.SetInt("MuteVoice", 0);
            }
            else if (elementIndex == 2)
            {
                if (rawImages[elementIndex].texture == firstTexture)
                    PlayerPrefs.SetInt("MuteMusic", 1);
                else
                    PlayerPrefs.SetInt("MuteMusic", 0);
            }
            else if (elementIndex == 3)
            {
                if (rawImages[elementIndex].texture == firstTexture)
                    PlayerPrefs.SetInt("MuteBackground", 1);
                else
                    PlayerPrefs.SetInt("MuteBackground", 0);
            }
            else if (elementIndex == 4)
            {
                if (rawImages[elementIndex].texture == firstTexture)
                    PlayerPrefs.SetInt("MuteUI", 1);
                else
                    PlayerPrefs.SetInt("MuteUI", 0);
            }
            AudioManager.Instance.GetSettings();
            SetSettings();
        }
    }
}
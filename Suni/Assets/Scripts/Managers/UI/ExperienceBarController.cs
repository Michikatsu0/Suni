using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarController : MonoBehaviour
{
    public Slider experienceBar;
    public float maxExperience = 1000f;
    [SerializeField] private float currentExperience = 0f;

    [SerializeField] private float experienceGained = 100f;
    [SerializeField] private int level = 1;

    public TMP_Text levelTex;
    public TMP_Text experienceGainedTex;

    public Material newFillMaterial;

    private void Start()
    {
        currentExperience = 0f;
        levelTex.text = ("Level " + level);
        experienceBar.fillRect.GetComponent<Image>().material = newFillMaterial;
    }

    public void GainExperience()
    {
        currentExperience += experienceGained;
        if (currentExperience >= maxExperience)
        {
            level++;
            levelTex.text = ("Level " + level);
            currentExperience = 0;
        }
        UpdateExperienceBar();
        StartCoroutine(SeeExperience());
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

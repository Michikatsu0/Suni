using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MeditationManager : MonoBehaviour
{
    public static MeditationManager Instance; 

    [SerializeField] private VideoPlayer suniVideoPlayer, backgroundVideoPlayer, frontgroundVideoPlayer;
    [SerializeField] private List<VideoClip> backgroundVideos = new List<VideoClip>();
    [SerializeField] private List<VideoClip> suniVideos = new List<VideoClip>();
    [SerializeField] private List<Button> videoButtons = new List<Button>();
    [SerializeField] private Material frontShader, suniIdleShader, suniMeditationShader;
    [SerializeField] private VideoScaler videoScaler;
    private Image panelAlpha;

    private void Awake()
    {
        Instance = this;
        panelAlpha = GameObject.Find("Panel").GetComponent<Image>();
        var color = new Color(1f, 1f, 1f, 0f);
        panelAlpha.color = color;
        videoScaler.ChangeVideo(suniVideos[0]);
        videoScaler.meshRenderer.material = suniIdleShader;
        suniVideoPlayer.Play();
        backgroundVideoPlayer.clip = backgroundVideos[0];
        frontShader.SetFloat("_HueRange", 3f);

    }
    
    public void Start()
    {
        UIElementsManager.Instance.DisableUI();
        videoButtons[1].gameObject.SetActive(false);
        videoButtons[0].onClick.AddListener(PlayVideo);
        suniVideoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        videoScaler.ChangeVideo(suniVideos[1]);
        videoScaler.meshRenderer.material = suniMeditationShader;
        PlayerPrefs.SetInt("SetMeditation", 1);
        backgroundVideoPlayer.clip = backgroundVideos[1];
        backgroundVideoPlayer.loopPointReached += EndReachedBack;
        MenuManager.Instance.EnableMenu(false);
        var color = new Color(1f, 1f, 1f, 0f);
        panelAlpha.color = color;
        suniVideoPlayer.Play();
        videoButtons[0].gameObject.SetActive(false);
    }

    private void EndReachedBack(VideoPlayer source)
    {
        backgroundVideoPlayer.clip = backgroundVideos[2];
        frontShader.SetFloat("_HueRange", 0.61f);
    }

    public void EndReached(VideoPlayer vp)
    {
        videoScaler.ChangeVideo(suniVideos[0]);
        videoScaler.meshRenderer.material = suniIdleShader;
        MenuManager.Instance.EnableMenu();
        var color = new Color(1f, 1f, 1f, 1f);
        panelAlpha.color = color;
        videoButtons[1].gameObject.SetActive(true);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class MemoriesManager : MonoBehaviour
{
    [SerializeField] List<VideoPlayer> videosPlayer = new List<VideoPlayer>();
    [SerializeField] List<GameObject> uIElements = new List<GameObject>();
    [SerializeField] private VideoPlayer backgroundVideo;
    private Image panelAlpha;
    
    private void Awake()
    {
        
        panelAlpha = GameObject.Find("Panel").GetComponent<Image>();
    }
    private void Start()
    {
        AudioManager.Instance.musicVolumen = 1f;
    }

    public void PlayVideo(int anim)
    {
        AudioManager.Instance.musicVolumen = 0f;
        AudioManager.Instance.backgroundVolumen = 0f;
        backgroundVideo.Stop();
        if (anim >= videosPlayer.Count) return;
        EnableUI(false);
        videosPlayer[anim].gameObject.SetActive(true);
        videosPlayer[anim].loopPointReached += EndReached;
        videosPlayer[anim].Play();

        var color = new Color(1f, 1f, 1f, 0f);
        panelAlpha.color = color;
        return ;
    }

    public void EnableUI(bool enable)
    {
        foreach (var g in uIElements)
            g.SetActive(enable);
    }

    private void EndReached(VideoPlayer source)
    {
        AudioManager.Instance.musicVolumen = 1f;
        AudioManager.Instance.backgroundVolumen = 1f;
        backgroundVideo.Play();
        var color = new Color(1f, 1f, 1f, 1f);
        panelAlpha.color = color;
        source.gameObject.SetActive(false);
        EnableUI(true);
    }

}

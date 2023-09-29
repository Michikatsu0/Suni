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
    [SerializeField] private Image panel;

    public void PlayVideo(int anim)
    {
        if (anim >= videosPlayer.Count) return;
        EnableUI(false);
        videosPlayer[anim].loopPointReached += EndReached;
        videosPlayer[anim].Play();

        var color = new Color(1f, 1f, 1f, 0f);
        panel.color = color;
        return ;
    }

    public void EnableUI(bool enable)
    {
        foreach (var g in uIElements)
            g.SetActive(enable);
        
    }

    private void EndReached(VideoPlayer source)
    {
        var color = new Color(1f, 1f, 1f, 1f);
        panel.color = color;
        source.gameObject.SetActive(false);
        EnableUI(true);
    }

}

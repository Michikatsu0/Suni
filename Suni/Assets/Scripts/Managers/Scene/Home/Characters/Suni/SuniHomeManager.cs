using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SuniHomeManager : MonoBehaviour
{
    [SerializeField] private float delayToNext, probBreath = 0.1f, probStretch = 0.1f, probGreet = 0.1f, probIdle = 0.7f;
    [SerializeField] private List<VideoClip> suniVideos = new List<VideoClip>();
    private VideoScaler videoScaler;
    private float time, currentProb;

    void Start()
    {
        videoScaler = GetComponent<VideoScaler>();
    }

    void Update()
    {
        if (time >= delayToNext)
        {
            currentProb = Random.Range(0.0f, 1.0f);

            if (currentProb < probBreath && videoScaler.videoPlayer.clip != suniVideos[0])
            {
                videoScaler.ChangeVideo(suniVideos[0]);
                delayToNext = 3f;
            }
            else if (currentProb < probStretch + probStretch && videoScaler.videoPlayer.clip != suniVideos[1])
            {
                videoScaler.ChangeVideo(suniVideos[1]);
                delayToNext = 3f;
            }
            else if (currentProb < probBreath + probStretch + probGreet && videoScaler.videoPlayer.clip != suniVideos[2])
            {
                videoScaler.ChangeVideo(suniVideos[2]);
                delayToNext = 3f;
            }
            else if (currentProb < probIdle && videoScaler.videoPlayer.clip != suniVideos[3])
            {
                videoScaler.ChangeVideo(suniVideos[3]);
                delayToNext = 3f;
            }

            time = 0;
        }
        else
            time += Time.deltaTime;
    }

}

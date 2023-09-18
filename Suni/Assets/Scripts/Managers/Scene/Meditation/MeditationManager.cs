using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MeditationManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public GameObject startButton; 
    public GameObject secondButton; 

    public void Start()
    {
        secondButton.SetActive(false);

        Button startButtonComponent = startButton.GetComponent<Button>();
        startButtonComponent.onClick.AddListener(PlayVideo);

        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();

        startButton.SetActive(false);
    }

    public void EndReached(VideoPlayer vp)
    {
        secondButton.SetActive(true);
    }
}

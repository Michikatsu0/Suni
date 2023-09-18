using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SecondButton : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject startButton; 
    public GameObject secondButton;

    private void Start()
    {
        
        secondButton.SetActive(false);

        
        videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        secondButton.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartButton : MonoBehaviour
{
    public VideoPlayer videoPlayer; 

    public GameObject startButton; 
    public GameObject secondButton; 

    private void Start()
    {
        secondButton.SetActive(false);

        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(PlayVideo);
    }

    public void PlayVideo()
    {
        
        videoPlayer.Play();

       
        startButton.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MeditationManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private Button startButton;
    private Button exitButton;
    private Button homeButton;

    private void Awake()
    {
        startButton = GameObject.Find("Button_Start").GetComponent<Button>();
        exitButton = GameObject.Find("Button_Exit").GetComponent<Button>();
        homeButton = GameObject.Find("Button_Home").GetComponent<Button>();
    }

    public void Start()
    {
        UIElementsManager.Instance.DisableUI();
        exitButton.gameObject.SetActive(false);
        startButton.onClick.AddListener(PlayVideo);
        videoPlayer.loopPointReached += EndReached;

        // Configura el video para iniciar en pausa
        videoPlayer.Pause(); // Pausa el video al inicio
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        startButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
    }

    public void EndReached(VideoPlayer vp)
    {
        exitButton.gameObject.SetActive(true);
    }
}

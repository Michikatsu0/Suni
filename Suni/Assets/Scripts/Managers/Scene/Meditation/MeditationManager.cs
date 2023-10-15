using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MeditationManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; 
    [SerializeField] private List<Button> videoButtons = new List<Button>();
    private Image panelAlpha;

    private void Awake()
    {
        panelAlpha = GameObject.Find("Panel").GetComponent<Image>();
        videoPlayer.Play();
        videoPlayer.Pause();
    }
    
    public void Start()
    {
        AudioManager.Instance.musicVolumen = 1f;
        UIElementsManager.Instance.DisableUI();
        videoButtons[1].gameObject.SetActive(false);
        videoButtons[0].onClick.AddListener(PlayVideo);
        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        AudioManager.Instance.musicVolumen = 0.1f;
        MenuManager.Instance.EnableMenu(false);
        var color = new Color(1f, 1f, 1f, 0f);
        panelAlpha.color = color;
        videoPlayer.Play();
        videoButtons[0].gameObject.SetActive(false);
        
    }

    public void EndReached(VideoPlayer vp)
    {
        AudioManager.Instance.musicVolumen = 1f;
        MenuManager.Instance.EnableMenu();
        var color = new Color(1f, 1f, 1f, 1f);
        panelAlpha.color = color;
        videoButtons[1].gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MeditationManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    [SerializeField] private List<Button> videoButtons = new List<Button>();
    private Image panelAlpha;
    private void Awake()
    {
        panelAlpha = GameObject.Find("Panel").GetComponent<Image>();
    }
    
    public void Start()
    {
        UIElementsManager.Instance.DisableUI();
        videoButtons[1].gameObject.SetActive(false);
        videoButtons[0].onClick.AddListener(PlayVideo);
        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        var color = new Color(1f, 1f, 1f, 0f);
        panelAlpha.color = color;
        videoPlayer.Play();
        videoButtons[0].gameObject.SetActive(false);
        videoButtons[2].gameObject.SetActive(false);
    }

    public void EndReached(VideoPlayer vp)
    {
        var color = new Color(1f, 1f, 1f, 1f);
        panelAlpha.color = color;
        videoButtons[1].gameObject.SetActive(true);
        videoButtons[2].gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
    }
}

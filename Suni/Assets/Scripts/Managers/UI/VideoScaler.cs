using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScaler : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform canvasScale; 
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        videoPlayer.loopPointReached += OnVideoLoopPointReached;
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        AdjustAspectRatio();
    }

    void OnVideoLoopPointReached(VideoPlayer source)
    {
        AdjustAspectRatio();
    }

    void AdjustAspectRatio()
    {
        var localScale = canvasScale.sizeDelta;
        rectTransform.localScale = new Vector2(localScale.x, localScale.y);
    }
}

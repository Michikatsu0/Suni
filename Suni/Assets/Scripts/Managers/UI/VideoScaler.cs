using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScaler : MonoBehaviour
{
    [SerializeField] private RectTransform canvasScale; 
    [HideInInspector] public VideoPlayer videoPlayer;
    private RenderTexture renderTexture;
    private RectTransform quadRectTransform;
    public Material shader;
    [HideInInspector] public MeshRenderer meshRenderer;

    private void Awake()
    {
        quadRectTransform = GetComponent<RectTransform>();
        meshRenderer = GetComponent<MeshRenderer>();
        videoPlayer = GetComponent<VideoPlayer>();
        AdjustAspectRatio();
        CreateTexture();
    }

    private void Update()
    {
        AdjustAspectRatio();
    }

    void AdjustAspectRatio()
    {
        quadRectTransform.localScale = canvasScale.sizeDelta;
    }
    
    public void CreateTexture()
    {
        renderTexture = new RenderTexture(1080, 1920, 0, DefaultFormat.LDR);
        videoPlayer.targetTexture = renderTexture;
        shader?.SetTexture("_MainTexture", renderTexture);
    }

    public void ChangeVideo(VideoClip newVideoClip)
    {
        if (!videoPlayer.targetTexture || shader.GetTexture("_MainTexture") == null) CreateTexture();
        videoPlayer.Stop();
        videoPlayer.clip = newVideoClip;
        videoPlayer.Play();
    }
}

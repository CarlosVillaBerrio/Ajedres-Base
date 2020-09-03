using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WorldSpaceVideo : MonoBehaviour
{
    VideoPlayer videoPlayer;

    public Material playButtonMaterial;
    public Material pauseButtonMaterial;
    public Renderer screenRenderer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            screenRenderer.material = playButtonMaterial;
        }
        else
        {
            videoPlayer.Play();
            screenRenderer.material = pauseButtonMaterial;

        }
    }
}

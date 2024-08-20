using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematica : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer;
    [SerializeField] string videoFileName;
    [SerializeField] long frameCount;
    [SerializeField] long playerCurrentFrame;
    public bool cinematicStarted = false;
    public int nextScene;

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void ContinueVideo()
    {
        videoPlayer.Play();
    }

    public void SkipVideo()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void Start()
    {
        PlayVideo();
    }

    public void PlayVideo()
    {
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }

    void Update()
    {
            playerCurrentFrame = videoPlayer.frame;
            frameCount = Convert.ToInt64(videoPlayer.frameCount);

            if (playerCurrentFrame >= frameCount - 1 && frameCount!=0)
            {
                SceneManager.LoadScene(nextScene);
            }             
    }
    
}

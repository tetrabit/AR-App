using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public RawImage videoTexture;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject playButton;

    void Start()
    {
        Application.runInBackground = true;
    }

    public void PlayVid()
    {
        if (videoPlayer && videoPlayer.isPrepared && videoPlayer.url != null)
        {
            videoPlayer.Play();
            playButton.SetActive(false);
        }
    }

    public void PauseVid()
    {
        if(videoPlayer && videoPlayer.isPrepared && videoPlayer.url != null)
        {
            StartCoroutine(PauseVideo());
        }
    }

    public IEnumerator PauseVideo()
    {
        //yield return new WaitForSeconds(1);

        yield return new WaitForEndOfFrame();
        videoPlayer.Pause();
        playButton.SetActive(true);
    }

    public IEnumerator NewVideo(string urls)
    {

        while(gameObject.GetComponent<VideoPlayer>() == null)
        {
            yield return null;
        }

        while (gameObject.GetComponent<AudioSource>() == null)
        {
            yield return null;
        }

        Debug.Log(urls);
        videoPlayer.url = urls;
        videoPlayer.Prepare();


        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoTexture.texture = videoPlayer.texture;
        Debug.Log(videoPlayer.texture);
        Debug.Log("Done Preparing " + urls);
        videoPlayer.Play();
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour {
    public RawImage imager;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public VideoSource videoSource;
    public Launchable.LaunchableTrackableEventHandler lteh;

    void Start () {
        lteh = GetComponent<Launchable.LaunchableTrackableEventHandler>();
        Application.runInBackground = true;
        StartCoroutine(SetupVideoPlayer());
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayVid()
    {
        if (videoPlayer && videoPlayer.isPrepared && videoPlayer.url != null)
        {
            videoPlayer.Play();
        }
    }

    public void PauseVid()
    {
        if(videoPlayer && videoPlayer.isPrepared && videoPlayer.url != null)
        {
            videoPlayer.Pause();
        }
    }



    IEnumerator SetupVideoPlayer()
    {

        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.Url;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        //Assign the Texture from Video to RawImage to be displayed
        imager.texture = videoPlayer.texture;
        lteh.SetVideoPlayer(gameObject.GetComponent<PlayVideo>());
        Debug.Log("Done setting up video player");

        yield return null;
    }

    public IEnumerator NewVideo(string urls) {

        while(videoPlayer == null)
        {
            yield return null;
        }

        while (audioSource == null)
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

        imager.texture = videoPlayer.texture;
        Debug.Log(videoPlayer.texture);
        Debug.Log("Done Preparing " + urls);
        videoPlayer.Play();
        audioSource.Play();

    }
}

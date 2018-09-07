﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayers : MonoBehaviour {
    public RawImage imager;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    private AudioSource audioSource;
    // Use this for initialization
    void Start () {
        Application.runInBackground = true;
        StartCoroutine(PlayVideo());
       // newVideo("https://s3.amazonaws.com/missways-application/Videos/VoW1.mp4");


    }

    IEnumerator PlayVideo()
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

        //videoPlayer.source = VideoSource.VideoClip;

        // Vide clip from Url
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";


        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        //videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        imager.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();
        audioSource.Play();


        


    }

    public IEnumerator NewVideo(string urls) {

        videoPlayer.url = urls;
        videoPlayer.Prepare();


        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        imager.texture = videoPlayer.texture;

        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDone Preparing Video");
        videoPlayer.Play();

    }


    // Update is called once per frame
    void Update () {
		
	}
}
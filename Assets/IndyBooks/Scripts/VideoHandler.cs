using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour {

    [SerializeField]
    VideoPlayer vp;


    public void PlayVid()
    {
        if(vp != null)
        {
            Debug.Log("video handler trying to play");
            vp.Play();
        }
    }

    public void PauseVid()
    {
        if(vp != null)
        {
            vp.Pause();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFullscreenVideo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Handheld.PlayFullScreenMovie("MissWaysSplashScreen.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
        if(PlayerPrefs.GetInt("tutorial") != 1)
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Cloud", LoadSceneMode.Single);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    string tutorial = "tutorial";

    public void TutorialHasBeenRan()
    {
        PlayerPrefs.SetInt(tutorial, 1);
        SceneManager.LoadScene("IndyBooks", LoadSceneMode.Single);
    }
}

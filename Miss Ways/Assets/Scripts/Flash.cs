using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Flash : MonoBehaviour {

    [SerializeField]
    GameObject flashOn;
    [SerializeField]
    GameObject flashOff;

    public void FlashOn()
    {
        flashOn.SetActive(true);
        flashOff.SetActive(false);
        CameraDevice.Instance.SetFlashTorchMode(true);
    }

    public void FlashOff()
    {
        flashOn.SetActive(false);
        flashOff.SetActive(true);
        CameraDevice.Instance.SetFlashTorchMode(false);
    }
}

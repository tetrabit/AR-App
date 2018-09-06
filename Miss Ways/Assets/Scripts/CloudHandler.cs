using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(CloudRecoBehaviour))]
public class CloudHandler : MonoBehaviour, ICloudRecoEventHandler
{

    private CloudRecoBehaviour cloudRecoBehaviour;
    private ObjectTracker mImageTracker;
    private bool mIsScanning = false;

    public ImageTargetBehaviour imageTargetTemplate;
    

    // Use this for initialization
    void Start () {
        cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        cloudRecoBehaviour.RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update () {
        
	}

    public void OnInitialized()
    {
        Debug.Log("Cloud Reco Initialized");
        mImageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
    }

    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.TargetFinder.ClearTrackables(false);
        }
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        throw new NotImplementedException();
    }
}

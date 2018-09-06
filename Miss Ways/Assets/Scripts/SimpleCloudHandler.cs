using System;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This MonoBehaviour implements the Cloud Reco Event handling for this sample.
/// It registers itself at the CloudRecoBehaviour and is notified of new search results.
/// </summary>
public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler
{
	#region PRIVATE_MEMBER_VARIABLES

	// CloudRecoBehaviour reference to avoid lookups
	private CloudRecoBehaviour cloudRecoBehaviour;
	// ImageTracker reference to avoid lookups
	private ObjectTracker mImageTracker;
    public Text texter;
	private bool mIsScanning = false;
    public RawImage video;
    public RawImage newVideo;
    #endregion // PRIVATE_MEMBER_VARIABLES



    #region EXPOSED_PUBLIC_VARIABLES

    /// <summary>
    /// can be set in the Unity inspector to reference a ImageTargetBehaviour that is used for augmentations of new cloud reco results.
    /// </summary>
    public ImageTargetBehaviour imageTargetTemplate;
	
	#endregion

	#region UNTIY_MONOBEHAVIOUR_METHODS

	/// <summary>
	/// register for events at the CloudRecoBehaviour
	/// </summary>
	void Start()
	{
        texter.text = "Cloud Reco initialized";
        // register this event handler at the cloud reco behaviour
        cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
		if (cloudRecoBehaviour)
		{
			cloudRecoBehaviour.RegisterEventHandler(this);
            texter.text = "Cloud Reco initialized";
        }


	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS


	#region ICloudRecoEventHandler_IMPLEMENTATION

	/// <summary>
	/// called when TargetFinder has been initialized successfully
	/// </summary>
	public void OnInitialized()
	{
        texter.text = "Cloud Reco initialized";
        // get a reference to the Image Tracker, remember it
        mImageTracker = (ObjectTracker)TrackerManager.Instance.GetTracker<ObjectTracker>();


    }

	/// <summary>
	/// visualize initialization errors
	/// </summary>
	public void OnInitError(TargetFinder.InitState initError)
	{
        texter.text = "Cloud Reco init error " + initError.ToString();
    }

	/// <summary>
	/// visualize update errors
	/// </summary>
	public void OnUpdateError(TargetFinder.UpdateState updateError)
	{
        texter.text = "Cloud Reco update error " + updateError.ToString();

    }

    /// <summary>
    /// when we start scanning, unregister Trackable from the ImageTargetTemplate, then delete all trackables
    /// </summary>
    public void OnStateChanged(bool scanning) {
		mIsScanning = scanning;
       // texter.text = "Cloud Reco State Changed is scanning" + scanning;
        if (scanning) {
			// clear all known trackables
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			tracker.TargetFinder.ClearTrackables (true);
		}
	}

	public MetadataParse mParse;
	public GameObject newImageTarget;
	/// <summary>
	/// Handles new search results
	/// </summary>
	/// <param name="targetSearchResult"></param>
	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
	{
		// duplicate the referenced image target
		newImageTarget = Instantiate(imageTargetTemplate.gameObject, Vector3.zero, Quaternion.Euler(90, 0, 0)) as GameObject;
		GameObject augmentation = null;

		string metaData = targetSearchResult.MetaData;
        Debug.Log("MetaData: \n" + metaData);
        texter.text = "Cloud Reco data:" + metaData;
        //StartCoroutine(video.GetComponent<VideoPlayers>().newVideo(metaData));
        //StartCoroutine(newImageTarget.GetComponent<PlayVideo>().newVideo(metaData));

        if (!mParse)
            Debug.Log("shit was null");
        mParse.ParseData(metaData, newImageTarget);



        if ( augmentation != null )
			augmentation.transform.parent = newImageTarget.transform;

        // enable the new result with the same ImageTargetBehaviour:
        
        ImageTargetBehaviour imageTargetBehaviour = mImageTracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget);

        if (!mIsScanning)
		{
            // stop the target finder
            cloudRecoBehaviour.CloudRecoEnabled = true;
		}
	}


	#endregion // ICloudRecoEventHandler_IMPLEMENTATION



}
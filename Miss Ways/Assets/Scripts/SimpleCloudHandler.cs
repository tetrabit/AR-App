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

	private bool mIsScanning = false;

	#endregion // PRIVATE_MEMBER_VARIABLES



	#region EXPOSED_PUBLIC_VARIABLES

	/// <summary>
	/// can be set in the Unity inspector to reference a ImageTargetBehaviour that is used for augmentations of new cloud reco results.
	/// </summary>
	public ImageTargetBehaviour ImageTargetTemplate;
	
	#endregion

	#region UNTIY_MONOBEHAVIOUR_METHODS

	/// <summary>
	/// register for events at the CloudRecoBehaviour
	/// </summary>
	void Start()
	{
        // register this event handler at the cloud reco behaviour
        cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
		if (cloudRecoBehaviour)
		{
			cloudRecoBehaviour.RegisterEventHandler(this);
		}


	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS


	#region ICloudRecoEventHandler_IMPLEMENTATION

	/// <summary>
	/// called when TargetFinder has been initialized successfully
	/// </summary>
	public void OnInitialized()
	{
		// get a reference to the Image Tracker, remember it
		mImageTracker = (ObjectTracker)TrackerManager.Instance.GetTracker<ObjectTracker>();


    }

	/// <summary>
	/// visualize initialization errors
	/// </summary>
	public void OnInitError(TargetFinder.InitState initError)
	{
    }

	/// <summary>
	/// visualize update errors
	/// </summary>
	public void OnUpdateError(TargetFinder.UpdateState updateError)
	{

    }

    /// <summary>
    /// when we start scanning, unregister Trackable from the ImageTargetTemplate, then delete all trackables
    /// </summary>
    public void OnStateChanged(bool scanning) {
		mIsScanning = scanning;
		if (scanning) {
			// clear all known trackables
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			tracker.TargetFinder.ClearTrackables (false);
		}
	}

	public metadataParse mParse;
	public GameObject newImageTarget;
	/// <summary>
	/// Handles new search results
	/// </summary>
	/// <param name="targetSearchResult"></param>
	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
	{
		// duplicate the referenced image target
		newImageTarget = Instantiate(ImageTargetTemplate.gameObject) as GameObject;
		GameObject augmentation = null;

		string metaData = targetSearchResult.MetaData;
        mParse.parseData(metaData);

		if( augmentation != null )
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
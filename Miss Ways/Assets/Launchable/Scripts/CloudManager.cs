using System;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This MonoBehaviour implements the Cloud Reco Event handling for this sample.
/// It registers itself at the CloudRecoBehaviour and is notified of new search results.
/// </summary>
namespace Launchable
{
    [RequireComponent(typeof(MetadataParse), typeof(CloudRecoBehaviour))]
    public class CloudManager : MonoBehaviour, ICloudRecoEventHandler
    {

        private MetadataParse mParse;
        public TargetFinder.TargetSearchResult tsr;


        // CloudRecoBehaviour reference to avoid lookups
        private CloudRecoBehaviour cloudRecoBehaviour;
        // ImageTracker reference to avoid lookups
        private ObjectTracker mImageTracker;
        private bool mIsScanning = false;

        #region UNTIY_MONOBEHAVIOUR_METHODS

        /// <summary>
        /// register for events at the CloudRecoBehaviour
        /// </summary>
        void Start()
        {
            mParse = gameObject.GetComponent<MetadataParse>();
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
        public void OnStateChanged(bool scanning)
        {
            mIsScanning = scanning;
            // texter.text = "Cloud Reco State Changed is scanning" + scanning;
            if (scanning)
            {
                // clear all known trackables
                TrackerManager.Instance.GetTracker<ObjectTracker>().TargetFinder.ClearTrackables(true);
            }
        }



        /// <summary>
        /// Handles new search results
        /// </summary>
        /// <param name="targetSearchResult"></param>
        public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
        {
            string metaData = targetSearchResult.MetaData;
            Debug.Log("MetaData: \n" + metaData);
            tsr = targetSearchResult;
            mParse.ParseData(metaData);

            Debug.Log(tsr.TargetName);
            if (!mIsScanning)
            {
                // stop the target finder
                cloudRecoBehaviour.CloudRecoEnabled = true;
            }
        }


        #endregion // ICloudRecoEventHandler_IMPLEMENTATION

        public void SetImageTargetFocus(GameObject target)
        {
            mImageTracker.TargetFinder.EnableTracking(tsr, target);
        }


    }
}
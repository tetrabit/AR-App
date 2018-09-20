using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

namespace Launchable
{
    public class ImageTargetManager : MonoBehaviour
    {
        //used if you want to generate a target
        [SerializeField]
        private GameObject baseImageTargetTemplate;

        //used for video targets
        [SerializeField]
        private GameObject videoImageTargetTemplate;

        //used for targets with 3D models
        [SerializeField]
        private GameObject modelImageTargetTemplate;

        //current target being generated/tracked
        [HideInInspector]
        public GameObject trackedTarget;

        [Serializable]
        public struct ModelDictionary
        {
            public string name;
            public GameObject model;
        }

        public ModelDictionary[] models;

        public void GenerateVideoPlayer(string url)
        {
            trackedTarget = Instantiate(videoImageTargetTemplate);
            StartCoroutine(trackedTarget.GetComponent<PlayVideo>().NewVideo(url));
            LaunchableManager.instance.CloudManager().SetImageTargetFocus(trackedTarget);
        }

        public void GenerateModel(string modelName)
        {
            for(int i = 0; i < models.Length; i++)
            {
                if(modelName == models[i].name)
                {
                    trackedTarget = Instantiate(models[i].model);
                    LaunchableManager.instance.CloudManager().SetImageTargetFocus(trackedTarget);
                }
            }
        }

        public void PauseVideo()
        {
            if(trackedTarget != null && trackedTarget.GetComponent<PlayVideo>() != null)
            {
                trackedTarget.GetComponent<PlayVideo>().PauseVid();
            }
        }

        /*
         *  
            trackedTarget = Instantiate(baseImageTargetTemplate);
            trackedTarget.transform.name = "Tracked Target Video";
            trackedTarget.AddComponent<Canvas>();
            trackedTarget.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            GameObject videoTexture = new GameObject();
            videoTexture.transform.parent = trackedTarget.transform;
            videoTexture.transform.localScale = new Vector3(1, 1, 1);
            videoTexture.transform.name = "Video Texture";
            videoTexture.AddComponent<RawImage>();
            videoTexture.transform.rotation = Quaternion.Euler(90, 0, 0);
            trackedTarget.AddComponent<VideoPlayer>();
            trackedTarget.AddComponent<AudioSource>();
            trackedTarget.GetComponent<VideoPlayer>().playOnAwake = false;
            trackedTarget.GetComponent<VideoPlayer>().source = VideoSource.Url;
            trackedTarget.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.AudioSource;
            trackedTarget.GetComponent<VideoPlayer>().EnableAudioTrack(0, true);
            trackedTarget.GetComponent<VideoPlayer>().SetTargetAudioSource(0, trackedTarget.GetComponent<AudioSource>());
            trackedTarget.AddComponent<PlayVideo>();
            videoTexture.GetComponent<RawImage>().texture = trackedTarget.GetComponent<VideoPlayer>().texture;
            StartCoroutine(trackedTarget.GetComponent<PlayVideo>().NewVideo(url));
            LaunchableManager.instance.CloudManager().SetImageTargetFocus(trackedTarget);
         * 
         * */
    }
}


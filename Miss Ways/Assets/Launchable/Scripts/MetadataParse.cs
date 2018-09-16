using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Launchable;
using UnityEngine.UI;

namespace Launchable
{
    public class MetadataParse : MonoBehaviour
    {
        // Use this for initialization
        public void ParseData(string metaData)
        {
            LaunchableManager.instance.CallToActionManager().ResetLinks();

            using (StringReader reader = new StringReader(metaData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ExecuteLineCommand(line);
                }
            }
        }


        public void ParseData(string metaData, GameObject imageTarget)
        {
            LaunchableManager.instance.CallToActionManager().ResetLinks();

            using (StringReader reader = new StringReader(metaData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ExecuteLineCommand(line, imageTarget);
                }
            }
        }

        IEnumerator WaitToPlay()
        {
            yield return new WaitForSeconds(1f);
            //Play video here
        }

        void ExecuteLineCommand(string line)
        {
            string[] splitMetadata = line.Split(' ');
            Debug.Log(line);
            if (splitMetadata[1].StartsWith("none")) return;

            switch (splitMetadata[0])
            {
                case "videoUrl":
                    Debug.Log("Video URL Parsed" + splitMetadata[1]);
                    LoadVideo(splitMetadata[1]);
                    break;
                case "3durl":
                    Debug.Log("3dURL Parsed" + splitMetadata[1]);
                    //Load3dAsset(splitMetadata[1]);
                    break;
                case "phone":
                    Debug.Log("Phone" + splitMetadata[1]);
                    DisplayPhoneLink(splitMetadata[1]);
                    break;
                case "website":
                    Debug.Log("Website" + splitMetadata[1]);
                    DisplayWebsiteLink(splitMetadata[1]);
                    break;
                case "email":
                    Debug.Log("email" + splitMetadata[1]);
                    DisplayEmailLink(splitMetadata[1]);
                    break;

                default:
                    break;
            }
        }

        void ExecuteLineCommand(string line, GameObject imageTarget)
        {
            string[] splitMetadata = line.Split(' ');
            Debug.Log(line);
            if (splitMetadata[1].StartsWith("none")) return;

            switch (splitMetadata[0])
            {
                case "videoUrl":
                    Debug.Log("Video URL Parsed" + splitMetadata[1]);
                    LoadVideo(splitMetadata[1], imageTarget);
                    break;
                case "3durl":
                    Debug.Log("3dURL Parsed" + splitMetadata[1]);
                    //Load3dAsset(splitMetadata[1]);
                    break;
                case "phone":
                    Debug.Log("Phone" + splitMetadata[1]);
                    DisplayPhoneLink(splitMetadata[1]);
                    break;
                case "website":
                    Debug.Log("Website" + splitMetadata[1]);
                    DisplayWebsiteLink(splitMetadata[1]);
                    break;
                case "email":
                    Debug.Log("email" + splitMetadata[1]);
                    DisplayEmailLink(splitMetadata[1]);
                    break;

                default:
                    break;
            }
        }

        void LoadVideo(string url)
        {
            //StartCoroutine(imageTarget.GetComponent<PlayVideo>().NewVideo(url));
            LaunchableManager.instance.ImageTargetManager().GenerateVideoPlayer(url);
        }

        void LoadVideo(string url, GameObject imageTarget)
        {
            //StartCoroutine(imageTarget.GetComponent<PlayVideo>().NewVideo(url));
            LaunchableManager.instance.ImageTargetManager().GenerateVideoPlayer(url);
        }

        void Load3dAsset(string url)
        {

        }

        void DisplayPhoneLink(string link)
        {
            LaunchableManager.instance.CallToActionManager().SetPhoneLink(link);
            LaunchableManager.instance.CallToActionManager().SetShowPhoneLink(true);
        }

        void DisplayWebsiteLink(string link)
        {
            LaunchableManager.instance.CallToActionManager().SetWebsiteLink(link);
            LaunchableManager.instance.CallToActionManager().SetShowWebsiteLink(true);
        }

        void DisplayEmailLink(string link)
        {
            LaunchableManager.instance.CallToActionManager().SetEmailLink(link);
            LaunchableManager.instance.CallToActionManager().SetShowEmailLink(true);
        }
    }
}


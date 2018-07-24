using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace EasyMobile.Demo
{
    public class Screenshot : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> UIToHide;
        [SerializeField]
        List<bool> UIStatus;
        [SerializeField]
        List<bool> UIShareScreen;

        Texture2D texture;
        [SerializeField]
        GameObject cubeParent;
        GameObject cube;
        [SerializeField]
        GameObject ImageHolder;

        // Screenshot names don't need to include the extension (e.g. ".png")
        string ScreenshotName = "IndybooksScreenshot";

        Texture2D screenCap;

        private void Awake()
        {
            screenCap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        }

        public void ScreenshotScreen()
        {
            for(int i = 0; i < UIStatus.Count; i++)
            {
                UIStatus[i] = UIToHide[i].activeSelf;
            }

            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(false);
            }

            VuforiaRenderer.Instance.Pause(true);

            StartCoroutine(PreviewScreenShot());


        }

        public void CameraView()
        {
            for(int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(UIStatus[i]);
            }
            ImageHolder.SetActive(false);

            VuforiaRenderer.Instance.Pause(false);
        }

        public void ShareScreenshot()
        {
            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(false);
            }

            StartCoroutine(CROneStepSharing());
        }

        public void SaveScreenshot()
        {
            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(false);
            }
            
            PlayerPrefs.SetInt("screenshot", PlayerPrefs.GetInt("screenshot")+1);
            
            ScreenshotName = ScreenshotName + PlayerPrefs.GetInt("screenshot");
            
            StartCoroutine(CRSaveScreenshot());
        }

        IEnumerator PreviewScreenShot()
        {
            yield return new WaitForEndOfFrame();

            screenCap.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenCap.Apply();

            ImageHolder.SetActive(true);
            ImageHolder.GetComponent<RawImage>().texture = screenCap;

            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(UIShareScreen[i]);
            }
        }

        IEnumerator CRSaveScreenshot()
        {
            yield return new WaitForEndOfFrame();

            ScreenshotName = "IndybooksScreenshot" + PlayerPrefs.GetInt("screenshot").ToString();
            Debug.Log(ScreenshotName);
            Sharing.SaveScreenshot(ScreenshotName);

            NativeUI.Alert("IndyBooks", "Picture Saved To Gallery");

            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(UIShareScreen[i]);
            }
        }
        
        IEnumerator CROneStepSharing()
        {
            yield return new WaitForEndOfFrame();
            ScreenshotName = "IndybooksScreenshot" + PlayerPrefs.GetInt("screenshot").ToString();
            Debug.Log(ScreenshotName);
            Sharing.SaveScreenshot(ScreenshotName);
            NativeGallery.LoadImageAtPath(Sharing.ShareScreenshot(ScreenshotName, ""), -1);

            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(UIShareScreen[i]);
            }
        }
    }
}


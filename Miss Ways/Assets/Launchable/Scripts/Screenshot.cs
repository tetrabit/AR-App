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

        [SerializeField]
        GameObject ImageHolder;

        public string albumName = "Launchable";

        // Screenshot names don't need to include the extension (e.g. ".png")
        private string screenshotPath;

        Texture2D screenshot;

        private void Awake()
        {
            screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
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

            StartCoroutine(CRShareScreenshot());
        }

        public void SaveScreenshot()
        {
            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(false);
            }
            StartCoroutine(CRSaveScreenshot());
        }

        IEnumerator PreviewScreenShot()
        {
            yield return new WaitForEndOfFrame();

            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();

            ImageHolder.SetActive(true);
            ImageHolder.GetComponent<RawImage>().texture = screenshot;

            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(UIShareScreen[i]);
            }

            StartCoroutine(CRSaveScreenshot());
        }

        IEnumerator CRSaveScreenshot()
        {
            yield return new WaitForEndOfFrame();

            if (PlayerPrefs.GetInt("screenshot") == null || PlayerPrefs.GetInt("screenshot") == 0)
            {
                PlayerPrefs.SetInt("screenshot", 1);
            }
            else
            {
                PlayerPrefs.SetInt("screenshot", PlayerPrefs.GetInt("screenshot") + 1);
            }

            screenshotPath = albumName + PlayerPrefs.GetInt("screenshot").ToString() + ".png";

            Debug.Log(screenshotPath);

            NativeGallery.SaveImageToGallery(screenshot, albumName, screenshotPath, null);
        }
        
        IEnumerator CRShareScreenshot()
        {
            NativeShare shareObject = new NativeShare();

            shareObject.SetSubject("A screenshot from the " + albumName + " app!");
            shareObject.SetText("A screenshot from the " + albumName + " app!");
            shareObject.AddFile(NativeGallery.GetSavePath(albumName, screenshotPath));
            shareObject.SetTitle(albumName + " Sharing");
            shareObject.Share();

            for (int i = 0; i < UIToHide.Count; i++)
            {
                UIToHide[i].SetActive(UIShareScreen[i]);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}


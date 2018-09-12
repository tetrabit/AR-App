using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Launchable
{
    public class CallToActionManager : MonoBehaviour
    {
        public static CallToActionManager instance = null;

        public GameObject email;
        public GameObject phone;
        public GameObject website;

        private string emailLink = "";
        private string phoneLink = "";
        private string websiteLink = "";

        private bool showEmailLink = false;
        private bool showPhoneLink = false;
        private bool showWebsiteLink = false;


        void Start()
        {
            //create singleton
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void HideLinks()
        {
            email.SetActive(false);
            phone.SetActive(false);
            website.SetActive(false);
        }

        public void ShowLinks()
        {
            email.gameObject.SetActive(showEmailLink);
            phone.gameObject.SetActive(showPhoneLink);
            website.gameObject.SetActive(showWebsiteLink);
        }

        public void ResetLinks()
        {
            showEmailLink = false;
            showPhoneLink = false;
            showWebsiteLink = false;
            ShowLinks();
        }

        public void SetShowEmailLink(bool show)
        {
            showEmailLink = show;
        }

        public void SetShowPhoneLink(bool show)
        {
            showPhoneLink = show;
        }

        public void SetShowWebsiteLink(bool show)
        {
            showWebsiteLink = show;
        }

        public void SetEmailLink(string link)
        {
            emailLink = "mailto:" + link;
        }

        public void SetPhoneLink(string link)
        {
            phoneLink = "tel://" + link;
        }

        public void SetWebsiteLink(string link)
        {
            websiteLink = link;
        }

        public void OnClickEmail()
        {
            if(emailLink != "")
            {
                Application.OpenURL(emailLink);
            }
        }

        public void OnClickPhone()
        {
            if(phoneLink != "")
            {
                Application.OpenURL(phoneLink);
            }
        }

        public void OnClickWebsite()
        {
            if(websiteLink != "")
            {
                Application.OpenURL(websiteLink);
            }
        }
    }
}

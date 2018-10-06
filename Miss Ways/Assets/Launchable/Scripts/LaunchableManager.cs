using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace Launchable
{
    [RequireComponent(typeof(CloudRecoBehaviour))]
    [RequireComponent(typeof(CloudManager))]
    [RequireComponent(typeof(MetadataParse))]
    [RequireComponent(typeof(ImageTargetManager))]
    [RequireComponent(typeof(CallToActionManager))]
    public class LaunchableManager : MonoBehaviour
    {
        public static LaunchableManager instance = null;

        private CloudRecoBehaviour crb;
        private CloudManager cm;
        private MetadataParse mp;
        private ImageTargetManager itm;
        private CallToActionManager cta;
        private AutoFocus af;
        private AreaPerimeter ap;

        void Start()
        {
            CreateSingleton();
            crb = GetComponent<CloudRecoBehaviour>();
            cm = GetComponent<CloudManager>();
            mp = GetComponent<MetadataParse>();
            itm = GetComponent<ImageTargetManager>();
            cta = GetComponent<CallToActionManager>();
            af = GetComponent<AutoFocus>();
            ap = GetComponent<AreaPerimeter>();
        }

        private void CreateSingleton()
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

        public CloudRecoBehaviour CloudRecoBehavior() { return crb; }
        public CloudManager CloudManager() { return cm; }
        public MetadataParse MetadataParse() { return mp; }
        public ImageTargetManager ImageTargetManager() { return itm; }
        public CallToActionManager CallToActionManager() { return cta; }
        public AutoFocus AutoFocus() { return af; }
        public AreaPerimeter AreaPerimeter() { return ap; }

    }
}


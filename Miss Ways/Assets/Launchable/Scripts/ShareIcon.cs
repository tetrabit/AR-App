using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareIcon : MonoBehaviour
{
    public Sprite android;
    public Sprite ios;
    public Image share;

    void Update()
    {
        share = GetComponent<Image>();
#if UNITY_IOS
        share.sprite = ios;
#endif

#if UNITY_ANDROID
        share.sprite = android;
#endif
    }


}

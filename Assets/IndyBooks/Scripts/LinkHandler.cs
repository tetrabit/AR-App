using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkHandler : MonoBehaviour {
    [SerializeField]
    GameObject Link;

    public void SetLinkActive()
    {
        Link.SetActive(true);
    }

    public void SetLinkInactive()
    {
        Link.SetActive(false);
    }
}

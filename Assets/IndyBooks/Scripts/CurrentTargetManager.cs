using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTargetManager : MonoBehaviour {

    [SerializeField]
    GameObject currentTarget;

    public void SetCurrentTarget(GameObject objectName)
    {
        currentTarget = objectName;
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }

    public void PlayVid()
    {
        if(currentTarget != null)
        {
            if (currentTarget.GetComponent<VideoHandler>() != null)
            {
                currentTarget.GetComponent<VideoHandler>().PlayVid();
            }
        }
    }

    public void PauseVid()
    {
        if(currentTarget != null)
        {
            if (currentTarget.GetComponent<VideoHandler>() != null)
            {
                currentTarget.GetComponent<VideoHandler>().PauseVid();
            }
        }
    }

    public void SetLinkActive()
    {
        if(currentTarget != null)
        {
            if (currentTarget.GetComponent<LinkHandler>() != null)
            {
                currentTarget.GetComponent<LinkHandler>().SetLinkActive();
            }
        }
    }

    public void SetLinkInactive()
    {
        if (currentTarget != null)
        {
            if (currentTarget.GetComponent<LinkHandler>() != null && currentTarget != null)
            {
                currentTarget.GetComponent<LinkHandler>().SetLinkInactive();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    Toggle t;

    public GameObject info;

    void Awake()
    {
        t = gameObject.GetComponent<Toggle>();
    }

    public void ToggleInfo()
    {
        info.SetActive(t.isOn);
    }
}

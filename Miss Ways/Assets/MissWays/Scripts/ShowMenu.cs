using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenu : MonoBehaviour {

    [SerializeField]
    List<GameObject> UIToHide;
    [SerializeField]
    List<bool> UIStatus;
    [SerializeField]
    List<bool> UIMenuScreen;

    public void MenuScreen()
    {
        for (int i = 0; i < UIStatus.Count; i++)
        {
            UIStatus[i] = UIToHide[i].activeSelf;
        }

        for (int i = 0; i < UIToHide.Count; i++)
        {
            UIToHide[i].SetActive(UIMenuScreen[i]);
        }
    }

    public void CameraView()
    {
        for (int i = 0; i < UIToHide.Count; i++)
        {
            UIToHide[i].SetActive(UIStatus[i]);
        }
    }
}

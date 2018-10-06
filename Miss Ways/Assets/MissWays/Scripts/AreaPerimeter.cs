using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaPerimeter : MonoBehaviour
{
    private HouseCardAnimationController hcac;
    public GameObject areaPerimeterUI;
    public Toggle area;
    public Toggle perimeter;
    public GameObject areaUI;
    public GameObject perimeterUI;

    public void Init()
    {
        areaPerimeterUI.SetActive(true);
        perimeterUI.SetActive(true);
        perimeter.isOn = true;
        if(hcac != null)
        {
            hcac.PlayFence();
        }
    }

    public void Hide()
    {
        areaPerimeterUI.SetActive(false);
        areaUI.SetActive(false);
        perimeterUI.SetActive(false);
    }

    public void OnPerimeterClicked()
    {
        if(hcac != null)
        {
            hcac.PlayFence();
            perimeterUI.SetActive(true);
            areaUI.SetActive(false);
        }
    }

    public void OnAreaClicked()
    {
        if(hcac != null)
        {
            hcac.PlayHouse();
            areaUI.SetActive(true);
            perimeterUI.SetActive(false);
        }
    }

    public void SetAnimationController(HouseCardAnimationController house)
    {
        hcac = house;
    }
}

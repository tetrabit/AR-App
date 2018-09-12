﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AutoFocus : MonoBehaviour {
    public void TriggerAutofocusEvent()
    {
        // Trigger an autofocus event
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);

        // Then restore original focus mode
        StartCoroutine(RestoreOriginalFocusMode());
    }

    private IEnumerator RestoreOriginalFocusMode()
    {
        // Wait 1.5 seconds
        yield return new WaitForSeconds(1f);
        // Restore original focus mode
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
}
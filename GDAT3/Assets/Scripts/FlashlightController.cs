using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    [SerializeField] private bool flashlightOn = true;

    private void Start()
    {
        PlayerInputManager.Instance.OnFlashlightButtonPressed += ToggleFlashlight;
    }
    
    private void ToggleFlashlight(object sender, EventArgs e)
    {
        flashlightOn = !flashlightOn;
        
        flashlightLight.enabled = flashlightOn;
    }

}

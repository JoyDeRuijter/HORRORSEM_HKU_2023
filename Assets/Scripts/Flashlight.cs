using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [HideInInspector] public bool isOn;
    [SerializeField] private Light2D flashLight;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
                TurnOffFlashLight();
            else
                TurnOnFlashLight();
        }
    }

    public void TurnOnFlashLight()
    {
        flashLight.enabled = true;
        isOn = true;
    }

    public void TurnOffFlashLight()
    { 
        flashLight.enabled = false;
        isOn = false;
    }

}

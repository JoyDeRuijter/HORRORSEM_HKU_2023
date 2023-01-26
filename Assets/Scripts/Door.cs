using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;

    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if ((isActive && sr.enabled && !bc.enabled) || (!isActive && !sr.enabled && bc.enabled))
            return;
        else
            DisableAndEnable();
    }

    // Disables and enables doors based on the boolean isActive
    private void DisableAndEnable()
    {
        if (isActive)
        {
            sr.enabled = true;
            bc.enabled = false;
        }
        else
        {
            sr.enabled = false;
            bc.enabled = true;
        }
    }
}

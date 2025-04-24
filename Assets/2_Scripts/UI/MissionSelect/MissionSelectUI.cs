using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSelectUI : MonoBehaviour
{
    public StartPortal startPortal;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ClickStart()
    {
        startPortal.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickCancel()
    {
        startPortal.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
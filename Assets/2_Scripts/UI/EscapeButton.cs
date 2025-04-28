using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void YesClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void NoClicked()
    {
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MissionText : MonoBehaviour
{
    public static MissionText Instance;
    
    [SerializeField]
    TextMeshProUGUI bigText;


    private void Awake()
    {
        Instance = this;
      
    }

    private void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 1)
        {
            bigText.text = "Select Mission";
        }
        else
        {
            bigText.text = "Find and destroy the crystals.";
        }
    }
    
    public void TextUpdate(string mission)
    {
        bigText.text = mission;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class TitleUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI endText;
    public Color mouseOverColor=Color.white;
    public Color originalColor;
    private void Start()
    {
        originalColor = startText.color;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        startText.color = mouseOverColor;
        endText.color = mouseOverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startText.color = originalColor;
        endText.color = originalColor;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public abstract class TextButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TextMeshProUGUI buttonText;

    private Color mouseOverColor = Color.white;
    private Color originalColor;

    private void Start()
    {
        originalColor = buttonText.color;
    }

    public abstract void ButtonClicked();

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = mouseOverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor;
    }
}
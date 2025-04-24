using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMissionBook : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject interactUI;

    public void Interact()
    {
        interactUI.gameObject.SetActive(true);
    }

    public string InteractText()
    {
        return "[E] Mission Select";
    }
}
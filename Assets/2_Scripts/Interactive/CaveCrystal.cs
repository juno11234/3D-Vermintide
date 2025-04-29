using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCrystal : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject Door;
    public void Interact()
    {
        Destroy(Door);
        MissionText.Instance.TextUpdate("Find the Passage");
        Destroy(gameObject);
    }

    public string InteractText()
    {
        return "[E] destroy the crystal";
    }
}
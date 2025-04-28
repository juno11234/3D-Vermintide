using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPotion : MonoBehaviour, IInteractable
{
    [SerializeField]
    Potion potion;

    public void Interact()
    {
        potion.GetPotion();
        Destroy(gameObject);
    }

    public string InteractText()
    {
        return "[E] Get Potion";
    }
}
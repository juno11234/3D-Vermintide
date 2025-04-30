using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPotion : MonoBehaviour, IInteractable
{
    [SerializeField]
    Potion potion;

    public void Interact()
    {
        if(Player.CurrentPlayer.hasPotion) return;
        potion.GetPotion();
        Destroy(gameObject);
    }

    public string InteractText()
    {
        if (Player.CurrentPlayer.hasPotion) return "Already have the potion";
        return "[E] Get Potion";
    }
}
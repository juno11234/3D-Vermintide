using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAmmo : MonoBehaviour, IInteractable
{
    public Gun gun;
    public void Interact()
    {
       gun.GetAmmo(5);
       Destroy(gameObject);
    }

    public string InteractText()
    {
        return "[E] Get Ammo";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour,IInteractable
{    

    public void Interact()
    {
        
    }

    public string InteractText()
    {
        return "[E] Quest";
    }
}

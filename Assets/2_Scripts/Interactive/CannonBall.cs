using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (Player.CurrentPlayer.getCannonBall) return;

        Player.CurrentPlayer.getCannonBall = true;
        Destroy(gameObject);
    }

    public string InteractText()
    {
        return "[E] Pick up";
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAttack : MonoBehaviour
{
    public Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player.CurrentPlayer.MainCollider))
        {
            Debug.Log("발!");
        }
    }
}
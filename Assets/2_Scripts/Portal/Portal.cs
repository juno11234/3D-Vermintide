using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        if (other.Equals(Player.CurrentPlayer.MainCollider)&& current == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if(other.Equals(Player.CurrentPlayer.MainCollider)&& current == 2)
        {
            SceneManager.LoadScene(1);
        }
    }
}
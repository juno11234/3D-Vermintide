using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ParticleSystem fire;

    [SerializeField]
    private GameObject rock;

    private int fireCount = 0;

    public void Interact()
    {
        if (Player.CurrentPlayer.getCannonBall)
        {
            fire.Play();
            fireCount++;
            Player.CurrentPlayer.getCannonBall = false;
            if (fireCount < 2)
            {
                WaveSystem.Instance.WaveStart(15);
                WaveSystem.Instance.set.waveDelay = 15;
            }
            if (fireCount > 2)
            {
                WaveSystem.Instance.set.waveDelay = 180;
                Destroy(rock);
                this.enabled = false;
            }
        }
    }

    public string InteractText()
    {
        if (Player.CurrentPlayer.getCannonBall)
        {
            return "[E] Fire";
        }
        else
        {
            return "need Cannonball";
        }
    }
}
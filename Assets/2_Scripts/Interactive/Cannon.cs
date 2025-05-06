using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ParticleSystem fire;

    [SerializeField]
    private GameObject rock;

    [SerializeField]
    private SFXData fireSFX;

    private int fireCount = 0;

    public void Interact()
    {
        if (Player.CurrentPlayer.getCannonBall)
        {
            fire.Play();
            SFXManager.Instance.Play(fireSFX);
            fireCount++;
            Player.CurrentPlayer.getCannonBall = false;
            if (fireCount < 2)
            {
                MissionText.Instance.TextUpdate($"Find the Cannonball and fire it. {fireCount} / 3 ");
                WaveSystem.Instance.WaveStart(10);
                WaveSystem.Instance.set.waveDelay = 20;
            }

            else if (fireCount == 2)
                MissionText.Instance.TextUpdate($"Find the Cannonball and fire it. {fireCount} / 3 ");

            else if (fireCount > 2)
            {
                MissionText.Instance.TextUpdate("Destroy the staff");
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
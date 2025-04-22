using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
   public WaveSystem waveSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player.CurrentPlayer.MainCollider))
        {
            waveSystem.WaveStart();
            Destroy(gameObject);
        }
    }
}

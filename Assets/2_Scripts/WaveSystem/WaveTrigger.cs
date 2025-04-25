using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
  
   public int waveAmountSet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player.CurrentPlayer.MainCollider))
        {
            WaveSystem.Instance.WaveStart(waveAmountSet);
            Destroy(gameObject);
        }
    }
}

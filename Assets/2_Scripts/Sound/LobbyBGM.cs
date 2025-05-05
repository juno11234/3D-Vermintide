using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBGM : MonoBehaviour
{
   
    void Start()
    {
        BGMManager.Instance.ChangeBGM(GameState.Idle);
    }

  
}

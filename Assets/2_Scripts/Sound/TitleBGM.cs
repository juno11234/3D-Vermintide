using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    
    void Start()
    {
        BGMManager.Instance.ChangeBGM(GameState.Title);
    }

}

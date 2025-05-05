using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBGM : MonoBehaviour
{
    void Start()
    {
        BGMManager.Instance.ChangeBGM(GameState.Dungeon);
    }
 
}

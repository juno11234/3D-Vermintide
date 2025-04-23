using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{    
    void Awake()
    {
       gameObject.SetActive(false);
    }

    void Update()
    {
        if (Boss.CurrentBoss.isDead)
        {
            gameObject.SetActive(true);
        }
    }
}

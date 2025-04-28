using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    private Portal portal;

    void Awake()
    {
        portal = GetComponentInChildren<Portal>();
        portal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Boss.CurrentBoss.isDead)
        {
            portal.gameObject.SetActive(true);
        }
    }
}
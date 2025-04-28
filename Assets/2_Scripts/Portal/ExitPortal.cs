using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    private Portal portal;

    [SerializeField]
    private GameObject cLoseDoor;
    void Awake()
    {
        portal = GetComponentInChildren<Portal>();
        portal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Boss.CurrentBoss.isDead)
        {
            MissionText.Instance.TextUpdate("Escape to the starting position!");
            Destroy(cLoseDoor);
            WaveSystem.Instance.WaveStart(15);
            WaveSystem.Instance.set.waveDelay = 15;
            portal.gameObject.SetActive(true);
        }
    }
}
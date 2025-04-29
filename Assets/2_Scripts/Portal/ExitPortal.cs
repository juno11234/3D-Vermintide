using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    private Portal portal;
    private int count = 0;

    [SerializeField]
    private GameObject cLoseDoor;

    void Awake()
    {
        portal = GetComponentInChildren<Portal>();
        portal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Boss.CurrentBoss.isDead && count == 0)
        {
            MissionText.Instance.TextUpdate("Escape to the starting position!");
            Destroy(cLoseDoor);
            WaveSystem.Instance.WaveStart(15);

            WaveSystem.Instance.set.waveDelay = 10;
            portal.gameObject.SetActive(true);
            count++;
        }
    }
}
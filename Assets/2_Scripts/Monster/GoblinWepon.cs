using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWepon : MonoBehaviour
{
  private Collider collider;

  private void Start()
  {
    collider = GetComponent<Collider>();
    collider.enabled = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log("고블린 공격!");
    }
  }
}

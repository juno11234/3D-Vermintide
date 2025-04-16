using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MonoBehaviour
{
    //무기 데미지, 공격시 콜라이더 활성화, 맞은 대상 몬스터 확인후 데미지

    private Collider collider;
    private int damage = 10;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("공격!");
        }
    }
}
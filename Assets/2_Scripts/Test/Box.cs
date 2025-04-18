using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    protected int num = 3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TestLog();
        }
    }

    private void TestLog()
    {
        Debug.Log(gameObject.name);
        Debug.Log(num);
    }
}
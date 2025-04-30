using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem attack1;

    [SerializeField]
    private ParticleSystem attack2;

    public void PlayParticle_1()
    {
        attack1.Play();
    }

    public void PlayParticle_2()
    {
        attack2.Play();
    }
}
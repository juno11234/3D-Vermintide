using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXData", menuName = "Sound/SFXData")]
public class SFXData : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    [Range(0f, 0.5f)]
    public float skip;
    [Range(0, 2)]
    public float volume = 1f;
}
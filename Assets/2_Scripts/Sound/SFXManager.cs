using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField]
    private int poolSize = 10;

    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            var sources = gameObject.AddComponent<AudioSource>();
            audioSources.Add(sources);
        }
    }

    public void PlayNoDuplicate(SFXData sfxData)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying && source.clip == sfxData.clip)
                return;
        }

        foreach (var sources in audioSources)
        {
            if (sources.isPlaying == false)
            {
                sources.clip = sfxData.clip;
                sources.pitch = Random.Range(0.95f, 1.05f);
                sources.volume = sfxData.volume;
                if (sfxData.skip > 0f)
                {
                    sources.time = sfxData.skip;
                }

                sources.Play();
                return;
            }
        }
    }

    public void Play(SFXData sfxData)
    {
        foreach (var sources in audioSources)
        {
            if (sources.isPlaying == false)
            {
                sources.clip = sfxData.clip;
                sources.volume = sfxData.volume;
                if (sfxData.skip > 0f)
                {
                    sources.time = sfxData.skip;
                }

                sources.Play();
                return;
            }
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SoundManager : Singleton<SoundManager> 
{
    public List<AudioSource> efxSources;
    public AudioSource musicSource;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

	// Use this for initialization
	public override void Awake ()
    {
        base.Awake();
    }
    public void Update()
    {
        
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaySingle(AudioClip clip, int maxRepititions = 1)
    {
        var efxSource = GetEfxSource(clip, maxRepititions);
        if (efxSource != null)
        {
            efxSource.clip = clip;
            efxSource.Play();
        }
    }

    public void RandomizeSfx(int maxRepititions, params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPatch = Random.Range(lowPitchRange, highPitchRange);

        var efxSource = GetEfxSource(clips[randomIndex], maxRepititions);
        if (efxSource != null)
        {
            efxSource.pitch = randomPatch;
            efxSource.clip = clips[randomIndex];
            efxSource.Play();
        }
    }

    public AudioSource GetEfxSource(AudioClip clip, int maxRepititions)
    {
        var isAlreadyPlaying = efxSources.Count(s => s.clip == clip && s.isPlaying) >= maxRepititions;
        if (isAlreadyPlaying) return null;

        var areAllUnavailable = efxSources.All(s => s.isPlaying);
        if (!areAllUnavailable)
        {
            return efxSources.First(s => !s.isPlaying);
        }
        else
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.enabled = true;            
            efxSources.Add(newSource);
            return newSource;
        }           
    }

}

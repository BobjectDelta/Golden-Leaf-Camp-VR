using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Play from a list of sounds using next, previous, and random
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlaySoundsFromList : MonoBehaviour
{
    [Tooltip("Loop the currently playing sound")]
    public bool shouldLoop = false;

    [Tooltip("The list of audio clips to play from")]
    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource = null;
    private int index = 0;

    [Header("Events")]
    public UnityEvent OnTrackChanged;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NextClip()
    {
        index = ++index % audioClips.Count;
        PlayClip();
    }

    public void PreviousClip()
    {
        index = Mathf.Abs(--index % audioClips.Count);
        PlayClip();
    }

    public void RandomClip()
    {
        index = Random.Range(0, audioClips.Count);
        PlayClip();
    }

    public void PlayAtIndex(int value)
    {
        index = Mathf.Clamp(value, 0, audioClips.Count);
        PlayClip();
    }

    public void TogglePause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.Play();
    }

    public void StopClip()
    {
        audioSource.Stop();
    }

    public void PlayCurrentClip()
    {
        PlayClip();
    }

    private void PlayClip()
    {
        audioSource.clip = audioClips[Mathf.Abs(index)];
        audioSource.Play();
        OnTrackChanged?.Invoke();
    }

    private void OnValidate()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.loop = shouldLoop;
    }

    public int GetCurrentTrackIndex()
    {
        return index;
    }
}

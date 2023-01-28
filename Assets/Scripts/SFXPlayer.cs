using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip longExhale;
    [SerializeField] AudioClip longInhale;
    [SerializeField] AudioClip smallInhale;
    [SerializeField] AudioClip longBlow;
    [SerializeField] AudioClip shortBlow;
    [SerializeField] AudioClip painedInhale;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDoorOpen()
    {
        audioSource.PlayOneShot(doorOpen);
    }

    public void PlayLongExhale()
    {
        audioSource.PlayOneShot(longExhale);
    }

    public void PlayLongInhale()
    {
        audioSource.PlayOneShot(longInhale);
    }

    public void PlaySmallInhale()
    {
        audioSource.PlayOneShot(smallInhale);
    }

    public void PlayLongBlow()
    {
        audioSource.PlayOneShot(longBlow);
    }

    public void PlayShortBlow()
    {
        audioSource.PlayOneShot(shortBlow);
    }

    public void PlayPainedInhale()
    {
        audioSource.PlayOneShot(painedInhale);
    }
}

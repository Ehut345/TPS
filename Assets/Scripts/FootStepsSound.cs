using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Footsteps Sources")]
    [SerializeField] private AudioClip[] footStepsSounds;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }
    AudioClip GetRandomFootStep()
    {
        return footStepsSounds[Random.Range(0, footStepsSounds.Length)];
    }
}

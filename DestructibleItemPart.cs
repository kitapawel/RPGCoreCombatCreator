using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

public class DestructibleItemPart : MonoBehaviour {

    [SerializeField] AudioClip[] hitSounds;
    AudioSource audioSource;

    void Start()
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        } else
        {
            audioSource = GetComponent<AudioSource>();
        }        
        //TODO consider adding the same check as above for RigidBody component and possibly collider
    }

    void Update()
    {

    }

    private void PlayHitSound()
    {
        var clip = hitSounds[Random.Range(0, hitSounds.Length)];
        audioSource.PlayOneShot(clip);
    }

    private void OnCollisionEnter()
    {
        PlayHitSound();
    }

}

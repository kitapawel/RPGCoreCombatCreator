using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;

    public class DestructibleItem : MonoBehaviour
    {        
        [SerializeField] GameObject brokenObject;
        [SerializeField] AudioClip[] hitSounds;
        [SerializeField] int structurePoints = 3;
        int currentStructurePoints;
        AudioSource audioSource;
        bool isDestroyed = false;
        public bool SetDestroyed(bool value)
        {
            if (value == true)
            {
                isDestroyed = true;
            }
            return isDestroyed;
        }

        void Start()
        {
            if (gameObject.GetComponent<AudioSource>() == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                audioSource = GetComponent<AudioSource>();
            }

            currentStructurePoints = structurePoints;
        }

        void Update()
        {
            if (currentStructurePoints <= 0)
            {
                GameObject plank = Instantiate(brokenObject, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        private void TakeDamage()
        {
            currentStructurePoints -= 1;
        }

        private void PlayHitSound()
        {
            var clip = hitSounds[Random.Range(0, hitSounds.Length)];
            audioSource.PlayOneShot(clip);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                TakeDamage();
                PlayHitSound();
                CameraShake.ShakeCamera(.1f, .5f); //TODO This line points to my CameraShake solution. You may use your own camera shake solution here or simply remove this line.
            }
        }

    }


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RPG.Characters
{
    public class ArmorSystem : MonoBehaviour
    {
        Animator animator;
        AudioSource audioSource;
        Character character;

        [Header ("Armor setup")]
        public ArmorWorn armorWorn;
        [SerializeField] AudioClip[] lightArmorHitSounds;
        [SerializeField] AudioClip[] mediumArmorHitSounds;
        [SerializeField] AudioClip[] heavyArmorHitSounds;
        
        void Start ()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public ArmorWorn GetArmorWorn()
        {
            return armorWorn;
        }

        public int GetArmorMitigation()
        {
            int armorMitigation;
            switch (armorWorn)
            {
                case ArmorWorn.None:
                    print ("None");
                    armorMitigation = 0;
                    break;
                case ArmorWorn.Light:
                    print ("Light");
                    armorMitigation = 1;
                    break;
                case ArmorWorn.Medium:
                    print ("Medium");
                    armorMitigation = 2;
                    break;
                case ArmorWorn.Heavy:
                    print ("Heavy");
                    armorMitigation = 3;
                    break;
                default:
                    armorMitigation = 0;
                    print ("Error - no armor type selected. How the F is this even possible?");
                    break;
            }
            return armorMitigation;
        }

        public int GetArmorGlancingChance()
        {
            int armorGlancingChance;
            switch (armorWorn)
            {
                case ArmorWorn.None:
                    armorGlancingChance = 1;
                    break;
                case ArmorWorn.Light:
                    print("Light");
                    armorGlancingChance = 4;
                    break;
                case ArmorWorn.Medium:
                    print("Medium");
                    armorGlancingChance = 7;
                    break;
                case ArmorWorn.Heavy:
                    print("Heavy");
                    armorGlancingChance = 10;
                    break;
                default:
                    armorGlancingChance = 1;
                    print("Error - no armor type selected. How the F is this even possible?");
                    break;
            }
            return armorGlancingChance;
        }

        public int GetArmorStaminaMod()
        {
            int armorStaminaMod;
            switch (armorWorn)
            {
                case ArmorWorn.None:
                    armorStaminaMod = 0;
                    break;
                case ArmorWorn.Light:
                    print("Light");
                    armorStaminaMod = 0;
                    break;
                case ArmorWorn.Medium:
                    print("Medium");
                    armorStaminaMod = 1;
                    break;
                case ArmorWorn.Heavy:
                    print("Heavy");
                    armorStaminaMod = 2;
                    break;
                default:
                    armorStaminaMod = 0;
                    print("Error - no armor type selected. How the F is this even possible?");
                    break;
            }
            return armorStaminaMod;
        }

        public bool IsGlancingHit (int glancingChanceModifier = 0)
        {
            int randomRoll = UnityEngine.Random.Range(0, 100);
            int glanceChance = GetArmorGlancingChance() + glancingChanceModifier;
            print("Rolled: " + randomRoll + " glance chance: " + glanceChance);

            if (randomRoll > glanceChance)
            {
                return false;
            }
            else
            {
                return true;
            }                
        }

            public void PlayHitSound()
        {
            if (armorWorn == ArmorWorn.None || armorWorn == ArmorWorn.Light)
            {
                audioSource.PlayOneShot(lightArmorHitSounds[UnityEngine.Random.Range(0, lightArmorHitSounds.Length)]);
            }
            else if (armorWorn == ArmorWorn.Medium)
            {
                audioSource.PlayOneShot(mediumArmorHitSounds[UnityEngine.Random.Range(0, mediumArmorHitSounds.Length)]);
            }
            else if (armorWorn == ArmorWorn.Heavy)
            {
                audioSource.PlayOneShot(heavyArmorHitSounds[UnityEngine.Random.Range(0, heavyArmorHitSounds.Length)]);
            }
        } 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.Characters
{
    public class VitalitySystem : MonoBehaviour
    {

        HealthSystem myHealthSystem;
        SpecialAbilities mySpecialAbilities;

        [SerializeField] int maxVitality = 100;
        [SerializeField] int healthRegenCost = 3;
        [SerializeField] int staminaRegenCost = 2;
        [SerializeField] Image vitalityBar;

        int currentVitality;

        public float GetVitalityAsPercentage()
        {
            float vitalityAsPercentage = (float)currentVitality / (float)maxVitality;
            return vitalityAsPercentage;
        }

        void Start()
        {
            myHealthSystem = GetComponent<HealthSystem>();
            mySpecialAbilities = GetComponent<SpecialAbilities>();
            currentVitality = maxVitality;
            UpdateVitalityBar();
        }


        public void RegenerateHealth()
        {
           int healthToRegenerate = myHealthSystem.GetMaxHealth() - myHealthSystem.GetCurrentHealth();
           if (healthToRegenerate > 0 && currentVitality >= healthRegenCost)
            {
                myHealthSystem.Heal(1);
                UpdateVitalityAmount(-healthRegenCost);
                UpdateVitalityBar();
            }
        }

        public void RegenerateStamina()
        {
            int staminaToRegenerate = mySpecialAbilities.GetMaxStamina() - mySpecialAbilities.GetCurrentStamina();
            if (staminaToRegenerate > 0 && currentVitality >= staminaRegenCost)
            {
                mySpecialAbilities.RegainStamina(2);
                UpdateVitalityAmount(-staminaRegenCost);
                UpdateVitalityBar();
            }
        }

        void UpdateVitalityAmount(int amount)
        {
            currentVitality = currentVitality + amount;
        }

        void UpdateVitalityBar()
        {
            if (vitalityBar)
            {
                vitalityBar.fillAmount = GetVitalityAsPercentage();
            }
        }
    }
}


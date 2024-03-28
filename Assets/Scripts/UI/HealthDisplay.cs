using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


    public class HealthDisplay : MonoBehaviour // HealthDisplay script is used to update the InGame UI by observing the PlayerHealth Script and returning those values to appear on the UI
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI YouAreDeadText;
        [SerializeField] private Image bloodImage;
        [SerializeField] private float flashDuration = 0.5f;
        private float flashTimer = 0f;
        [SerializeField] private PlayerHealth playerhealth; // Reference to the PlayerHealth script

        void Start()
        {
            // Ensure the healthText and playerDamageable are properly assigned in the Unity Editor
            if (healthText == null || YouAreDeadText == null || playerhealth == null)
            {
                Debug.LogError("HealthDisplay: Assign the healthText or YouAreDeadText and playerDamageable in the Unity Editor!");
                return;
            }


            // Set the initial health text
            UpdateHealthText(playerhealth.GetPlayerCurrentHealth()); // Calling the method UpdateHealthText which gets and returns the playerhealth current health value. This is called at start to ensure the current health is displayed 
        }

        private void Update()
        {
            UpdateHealthText(playerhealth.GetPlayerCurrentHealth()); // Same as in start but this is for everyframe, so that any change to the players health will appear every frame

            if (flashTimer > 0)
            {
                flashTimer -= Time.deltaTime;
            }

            if(playerhealth.Playercurrenthealth <= 0f)
            {
                YouAreDeadText.text = "You are dead";
            }
            
        }

        void UpdateHealthText(float PlayercurrentHealth)
        {
            // Update the health text
            healthText.text = $"Health: {PlayercurrentHealth}";
            UpdateDamageFlash(PlayercurrentHealth);
        }

        void UpdateDamageFlash(float playerCurrentHealth) // Update the damage flash/Blood effect based on the player's current health
        {
            float alpha = 1f - Mathf.Clamp01(playerCurrentHealth / playerhealth.GetPlayerInitialHealth());
            Color bloodColor = bloodImage.color;
            bloodColor.a = alpha;
            bloodImage.color = bloodColor;
        }

        public void FlashBlood()
        {
            flashTimer = flashDuration;
        }

        


    }


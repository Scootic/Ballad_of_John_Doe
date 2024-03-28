using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damageables
{


    public class DamageableObject : MonoBehaviour, IDamage // Declaring that it is an IDamage, which needs to incorporate the method from the IDamage interface
    {
        [Header("Settings")]
        [SerializeField] public float startinghealth = 50f; // Objects starting health

        [SerializeField] public float currentHealth; // Objects current health at a given time

        private bool hasDied = false;

        

        [SerializeField] private GameObject deathParticles;
        [SerializeField] private GameObject damageParticles;
        private GameObject deathParticlesInstance;
        private GameObject damageParticlesInstance;
        private float particlesDestroyDelay = 3f;

        private bool hasPlayedDeathParticles = false;
        private bool hasPlayedDamageParticles = false;

        private AudioSource audioSource;
        [SerializeField] private AudioClip deathSound;

        void Start()
        {
            audioSource = GetComponent<AudioSource>(); // Get an audio source if it doesn't have one
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            currentHealth = startinghealth; // Setting the objects current health to its starting health on start
            
            
        }

        // IDamageable interface method
        public void TakeDamage(float damage)
        {
            Debug.Log($"{gameObject.name} took {damage} damage. Current Health: {currentHealth - 10f}");
            currentHealth -= damage; // currentHealth = currentHealth - damage

            // Visual and audio feedback using Scriptable Object settings
            //PlayDamageSound();
            //SpawnDamageParticles();

            if (currentHealth <= 0) // If the object reaches zero or below health, begin the die method
            {
                
                PlayDamageSound(deathSound);
                Die();
                
            }
        }

        public float GetCurrentHealth() // A good way to get an objects currentHealth from another script
        {
            return currentHealth;
        }

        // Method to play damage sound
        private void PlayDamageSound(AudioClip sound)
        {
            if (sound != null)
            {
                audioSource.enabled = true;
                
                
                audioSource.clip = deathSound;
                audioSource.Play();
                
                // Play the sound
                // Example: AudioSource.PlayOneShot(sound);
            }
        }

        // Method to spawn damage particles
        private void SpawnDamageParticles(GameObject particlesPrefab)
        {
            if (particlesPrefab != null)
            {
                // Instantiate and spawn particles using the provided prefab
                if (!hasPlayedDamageParticles)
                    damageParticlesInstance = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
                Destroy(damageParticlesInstance, particlesDestroyDelay);
            }
        }

       
        // Method to handle object destruction
        private void Die()
        {
            if (!hasDied)
            {
                hasDied = true;
                
                // Check if death particles have not been played before
                if (!hasPlayedDeathParticles && deathParticles != null)
                {
                    // Instantiate and store a reference to the death particle effect
                    deathParticlesInstance = Instantiate(deathParticles, transform.position, Quaternion.identity);

                    // Set the flag to indicate that death particles have been played
                    hasPlayedDeathParticles = true;

                    // Destroy the instantiated particles after a delay
                    Destroy(deathParticlesInstance, particlesDestroyDelay);
                }

                // Implement object death logic here
                Destroy(gameObject);
            }


        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class BasicEnemy : MonoBehaviour, IDamage // Declaring that it is an IDamage, which needs to incorporate the method from the IDamage interface
 {
    [Header("Settings")]
    [SerializeField] public float EnemystartHealth = 100f; // Enemy starting health

    [SerializeField] public float EnemycurrentHealth; // Enemy current health at a given time

    public void Start()
    {
        EnemycurrentHealth = EnemystartHealth; // At start, sets the EnemycurrentHealth to the EnemystartHealth
    }
    public void TakeDamage(float damage)  // IDamage interface method
      {
          Debug.Log($"Enemy took {damage} damage. Enemy has {EnemycurrentHealth - 10f} health.");
          EnemycurrentHealth -= damage; // EnemycurrentHealth = EnemycurrentHealth - damage

          if(EnemycurrentHealth <= 0) // If enemy health hits 0 or less, begin die method
          {
              Die();
          }
      }

      private void Die() // Simple die method that will be added to in the future
      {
          Destroy(gameObject);
      }

 }


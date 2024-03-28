using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damageAmount = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object implements the IDamage interface
        IDamage damageable = collision.gameObject.GetComponent<IDamage>();
        Debug.Log("Fireball hit something");

        // If the collided object implements the IDamage interface, apply damage
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
            Debug.Log("Hit damageable, Should Take Damage");
        }

        // Destroy the fireball on collision
        Destroy(gameObject);
    }
}

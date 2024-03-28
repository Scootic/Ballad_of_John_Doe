using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Pickup : BASE_Pickup
{
    [SerializeField] private FireballShooter meUPGRDAS;
    protected override void PickupBehavior(Collider collider)
    {
        Debug.Log("Pick up the fireball!");
        
        meUPGRDAS.AddAmmo(10);
        meUPGRDAS.Reload();
    }
}

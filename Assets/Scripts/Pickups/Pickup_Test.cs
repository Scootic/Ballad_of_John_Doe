using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Test : BASE_Pickup
{
    protected override void PickupBehavior(Collider collider)
    {
        Debug.Log("I was a picked up boy!");
    }
}

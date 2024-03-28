using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Pickup : BASE_Pickup
{
    [SerializeField,Min(0)] int jumpAmountIncrease = 1;
    protected override void PickupBehavior(Collider collider)
    {
        Debug.Log("I was a picked up boy!");
        collider.GetComponent<PlayerMovement>().increaseJumpAmount(jumpAmountIncrease);
    }
}

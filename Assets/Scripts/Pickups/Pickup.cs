using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PickupType
{
    Health,
    Ammo,
    AmmoUpgrade,
    SpeedBoost
}
public abstract class Pickup : MonoBehaviour
{
    public PickupType type;

    public abstract void Collect();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject currentKey = null;

    public void AddKey(GameObject key)
    {
        currentKey = key;
        // You can perform any additional logic here, such as displaying a message that the key has been picked up
    }

    public bool HasKey()
    {
        return currentKey != null;
    }

    // You can add more methods to use the key, drop the key, etc.
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoPickupTypes { SMALLAMMO, MEDIUMAMMO, LARGEAMMO }
public class AmmoPickups : MonoBehaviour
{
    public AmmoPickupTypes pickup;

    public int myValue;

    private GunClass equippedGun;

    // Start is called before the first frame update
    void Start()
    {
        equippedGun = InputManager.instance.equippedGun;
        if (equippedGun == null)
        {
            Debug.Log("No weapon is equipped");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && equippedGun.CurrentStoredAmmo < equippedGun.MaxStoredAmmo)
        {
            switch (pickup)
            {
                case AmmoPickupTypes.SMALLAMMO:
                    equippedGun.AddAmmo(myValue);
                    break;
                case AmmoPickupTypes.MEDIUMAMMO:
                    equippedGun.AddAmmo(myValue);
                    break;
                case AmmoPickupTypes.LARGEAMMO:
                    equippedGun.AddAmmo(myValue);
                    break;
            }

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        equippedGun = InputManager.instance.equippedGun;
    }
}

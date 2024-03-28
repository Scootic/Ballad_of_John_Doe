using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunClass : MonoBehaviour                       //GUN CLASS HIERARCHY
{
    // Common properties for all guns
    public abstract float Damage { get; }
    public abstract float Range { get; }
    public abstract float FireRate { get; }
    public abstract int MaxBulletsPerMagazine { get; }
    public abstract int MaxStoredAmmo { get; }
    public abstract float ReloadTime { get; }
    public abstract int CurrentBullets { get; }
    public abstract int CurrentStoredAmmo { get; }
    public abstract GameObject MuzzleFlashPrefab { get; } // Muzzle flash prefab
    public abstract AudioClip ShootSound { get; } // Shooting sound

    // Common SFX & Audio needs for all guns
    public GameObject muzzleflash;
    public AudioClip shootSound;

    // Abstract methods for shooting and reloading, to be implemented by derived classes
    public abstract void Shoot();
    public abstract void Reload();

    public abstract void AddAmmo(int amount);

    public abstract void AmmoUpgrade(int amount);
}

public abstract class GunState
{
    protected GunClass gun;

    public GunState(GunClass gun)
    {
        this.gun = gun;
    }

    public virtual void HandleShootInput()
    {

    }
    public virtual void HandleReloadInput()
    {

    }
    public virtual void HandleAddAmmoInput(int amount)
    {

    }
    public virtual void HandleAmmoUpgrade(int amount) { }
}

public class EquippedState : GunState
{
    public EquippedState(GunClass gun) : base(gun)
    {

    }

    public override void HandleShootInput()
    {
        gun.Shoot();
    }

    public override void HandleReloadInput()
    {
        gun.Reload();
    }
    public override void HandleAddAmmoInput(int amount)
    {
        gun.AddAmmo(amount);
    }
    public override void HandleAmmoUpgrade(int amount)
    {
        gun.AmmoUpgrade(amount);
    }
}

// Define a specific gun type (e.g., Pistol) as a subclass of Gun
/*public class Revolver : GunClass
{
    private int currentBullets;
    private float nextTimeToFire;
    private GameObject muzzleflashprefab;
    private Transform muzzleflashLocation;
    private GameObject muzzleFlashInstance;
    private AudioSource audioSource;
    private AudioClip shootAudio;
    private int currentStoredAmmo;
    private int maxStoredAmmo;

    private bool isReloading = false;

    public override float Damage => 30f;
    public override float Range => 300f;
    public override float FireRate => 5f; 
    public override int MaxBulletsPerMagazine => 6;
    public override float ReloadTime => 3f;
    
    

    public override GameObject MuzzleFlashPrefab => Resources.Load<GameObject>("RevolverMuzzleFlash");
    public override AudioClip ShootSound => Resources.Load<AudioClip>("RevolverShootSound");
    public override int CurrentBullets => currentBullets;
    public override int MaxStoredAmmo => maxStoredAmmo;
    public override int CurrentStoredAmmo => currentStoredAmmo;
    public Revolver()
    {
        
        currentBullets = MaxBulletsPerMagazine;
        currentStoredAmmo = maxStoredAmmo;
        muzzleflashprefab = MuzzleFlashPrefab;
    }

    
    // Implement shooting logic specific to the Revolver
    public override void Shoot()
    {
        // Revolver shooting logic
        if (isReloading)
        {
            Debug.Log("Reloading... can't shoot yet");
            return;
        }
        if (currentBullets <= 0)
        {
            Debug.Log("Weapon Empty!");
            return;
        }
        if (Time.time < nextTimeToFire || currentBullets <= 0)
            return;

        // Instantiate muzzle flash prefab at the muzzle flash location
        muzzleFlashInstance = Instantiate(MuzzleFlashPrefab, muzzleflashLocation.position, muzzleflashLocation.rotation);
        // Play shoot audio
        audioSource.PlayOneShot(shootAudio);

        // Perform a raycast to detect hits
        RaycastHit hit;
        if (Physics.Raycast(muzzleflashLocation.position, muzzleflashLocation.forward, out hit, Range))
        {
            // Handle hit detection, apply damage to the target, etc.
            Transform objectHit = hit.transform;
            MonoBehaviour[] mono = objectHit.gameObject.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour item in mono)
            {
                if (item is IDamage)
                {
                    IDamage temp = item as IDamage;
                    temp.TakeDamage(Damage);
                    break;
                }
            }
        }
    

        // Update next time the pistol can fire
        nextTimeToFire = Time.time + 1f / FireRate;
        // Reduce current bullets
        currentBullets--;
    }



    // Implement reloading logic specific to the Revolver
    public override void Reload()
    {
        // Revolver reloading logic
    }
}

/*public class DoubleBarrelShotgun : Gun
{
    private int currentBullets;
    private float nextTimeToFire;
    private GameObject muzzleflashprefab;
    private Transform muzzleflashLocation;
    private GameObject muzzleFlashInstance;
    private AudioSource audioSource;
    private AudioClip shootAudio;
    private int currentStoredAmmo;
    private int maxStoredAmmo;
    public override float Damage => 60f;
    public override float Range => 100f;
    public override float FireRate => 2f;
    public override int MaxBulletsPerMagazine => 2;
    public override float ReloadTime => 2.5f;
    public override GameObject MuzzleFlashPrefab => Resources.Load<GameObject>("DoubleBarrelShotgunMuzzleFlash");
    public override AudioClip ShootSound => Resources.Load<AudioClip>("DoubleBarrelShotgunShootSound");
    public override int CurrentBullets => currentBullets;

    // Implement shooting logic specific to the Pistol
    public override void Shoot()
    {
        // Shotgun shooting logic
    }

    // Implement reloading logic specific to the Pistol
    public override void Reload()
    {
        // Shotgun reloading logic
    }

}


public class WinchesterRifle : Gun
{
    private int currentBullets;
    private float nextTimeToFire;
    private GameObject muzzleflashprefab;
    private Transform muzzleflashLocation;
    private GameObject muzzleFlashInstance;
    private AudioSource audioSource;
    private AudioClip shootAudio;
    private int currentStoredAmmo;
    private int maxStoredAmmo;

    public override float Damage => 10f;
    public override float Range => 50f;
    public override float FireRate => 2f;
    public override int MaxBulletsPerMagazine => 10;
    public override float ReloadTime => 1.5f;
    public override GameObject MuzzleFlashPrefab => Resources.Load<GameObject>("WinchesterRifleMuzzleFlash");
    public override AudioClip ShootSound => Resources.Load<AudioClip>("WinchesterRifleShootSound");
    public override int CurrentBullets => currentBullets;

    // Implement shooting logic specific to the WinchesterRifle
    public override void Shoot()
    {
        // Rifle shooting logic
    }

    // Implement reloading logic specific to the WinchesterRifle
    public override void Reload()
    {
        // Rifle reloading logic
    }
}*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : GunClass
{

    public int currentBullets;
    [SerializeField] int fireballSpeed = 30;
    private float nextTimeToShoot;
    public GameObject muzzleflashprefab;
    public Transform muzzleflashLocation;
    private GameObject muzzleFlashInstance;
    private AudioSource audioSource;
    public AudioClip shootAudio;
    private int currentStoredAmmo = 1;
    public int maxStoredAmmo = 30;
    private Recoil recoil;

    // Set values for the fireball shooter
    public override float Damage => 100f;
    public override float Range => 200f;
    public override float FireRate => 2f;
    public override int MaxBulletsPerMagazine => 1; // Single shot
    
    public override float ReloadTime => 2f;

    

    public override GameObject MuzzleFlashPrefab => muzzleflashprefab;
    public override AudioClip ShootSound => shootAudio;
    public override int CurrentBullets => currentBullets;
    public override int MaxStoredAmmo => maxStoredAmmo;
    public override int CurrentStoredAmmo => currentStoredAmmo;

    // Prefab for fireball
    public GameObject fireballPrefab;

    public FireballShooter()
    {
        // Initialize current bullets and stored ammo
        currentBullets = MaxBulletsPerMagazine;
        maxStoredAmmo = MaxStoredAmmo;
        currentStoredAmmo = MaxStoredAmmo;
    }

    

    private void Awake()
    {
        // Get the AudioSource component or add one if it doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        recoil = GameObject.Find("CameraRot/CameraRecoil").GetComponent<Recoil>();
    }

    // Implement shooting logic specific to the fireball shooter
    public override void Shoot()
    {
        // Check if it's time to shoot
        if (Time.time < nextTimeToShoot || currentBullets <= 0)
            return;

        // Instantiate fireball prefab
        GameObject fireballInstance = Instantiate(fireballPrefab, muzzleflashLocation.transform.position, muzzleflashLocation.transform.rotation);
        // Give Recoil
        recoil.RecoilFire();
        // Get the rigidbody of the fireball
        Rigidbody fireballRigidbody = fireballInstance.GetComponent<Rigidbody>();

        Destroy(fireballInstance, 6f);

        // Check if the fireball prefab has a rigidbody
        if (fireballRigidbody != null)
        {
            // Add force to the fireball to make it move forward
            fireballRigidbody.AddForce(transform.forward * fireballSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Fireball prefab is missing a Rigidbody component!");
        }

        // Play shoot audio
        audioSource.PlayOneShot(shootAudio);

        // Update next time the fireball shooter can shoot
        nextTimeToShoot = Time.time + 1f / FireRate;

        // Reduce current bullets
        currentBullets--;
    }

    // Implement reloading logic specific to the fireball shooter
    public override void Reload()
    {
        if (currentStoredAmmo > 0 && currentBullets < MaxBulletsPerMagazine)
        {
            // Calculate bullets to reload
            int bulletsToReload = Mathf.Min(MaxBulletsPerMagazine - currentBullets, currentStoredAmmo);

            // Update current bullets and stored ammo
            currentBullets += bulletsToReload;
            currentStoredAmmo -= bulletsToReload;
        }
    }

    // Method to add ammo to the fireball shooter
    public override void AddAmmo(int amount)
    {
        if (currentStoredAmmo < maxStoredAmmo)
        {
            Debug.Log("currentStoredAmmo < maxStoredAmmo");
            currentStoredAmmo = Mathf.Min(currentStoredAmmo + amount, maxStoredAmmo);
        }
    }

    // Method to upgrade the maximum stored ammo of the fireball shooter
    public override void AmmoUpgrade(int amount)
    {
        maxStoredAmmo += amount;
        Debug.Log("Max stored ammo increased by " + amount);
    }
}



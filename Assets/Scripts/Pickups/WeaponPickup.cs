using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    Rifle,
    Shotgun
}

[System.Serializable]
public struct GunStats
{
    public float damage;
    public float range;
    public float fireRate;
    public int maxBullets;
    public float reloadTime;
    public int maxStoredBullets;
    
}

public class WeaponPickup : MonoBehaviour
{
    Transform fpsCam;
    [SerializeField] Transform muzzleflashLocation;
    private AudioSource audioSource;

    [Header("Visual Effects")]
    public GameObject muzzleFlash;

    [Header("Sound Effects")]
    public AudioClip shootAudio;

    private GameObject MuzzleFlashInstance;

    public GunStats pistolStats;
    public GunStats rifleStats;
    public GunStats shotgunStats;

    private GunStats currentGunStats;

    private int currentBullets;
    private int currentStoredAmmo;
    private bool isReloading = false;
    

    public GunType gunType; // Enum representing the type of gun

    private float nextTimeToFire = 0f;

    private AmmoDisplay ammoDisplay;

    public Rigidbody rb;
    public Collider coll;
    private Transform gunContainer;
    

    void Awake()
    {
        fpsCam = Camera.main.transform; // Get the main camera transform for raycasting
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Add an audioSource if it doesn't exist
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SetGunStats(gunType);
        currentBullets = currentGunStats.maxBullets;
        currentStoredAmmo = currentGunStats.maxStoredBullets;

        ammoDisplay = FindObjectOfType<AmmoDisplay>();
        if(ammoDisplay == null)
        {
            Debug.LogError("ammoDisplay cannot be found in scene!");
        }

        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        gunContainer = transform.parent;
    }

    void SetGunStats(GunType type)
    {
        switch (type)
        {
            case GunType.Pistol:
                currentGunStats = pistolStats;
                break;
            case GunType.Rifle:
                currentGunStats = rifleStats;
                break;
            case GunType.Shotgun:
                currentGunStats = shotgunStats;
                break;
        }
    }

    public void Shoot()
    {
      
        if (isReloading)
        {
            Debug.Log("Reloading... Can't shoot!");
            return;
        }

        if (currentBullets <= 0)
        {
            Debug.Log("Magazine Empty");
            return;
        }

        if (Time.time < nextTimeToFire)
        {
            return;
        }

        MuzzleFlashInstance = Instantiate(muzzleFlash, muzzleflashLocation.position, Quaternion.identity);
        PlayAudio();
        Destroy(MuzzleFlashInstance, 2f);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, currentGunStats.range))
        {
            Transform objectHit = hit.transform;
            MonoBehaviour[] mono = objectHit.gameObject.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour item in mono)
            {
                if (item is IDamage)
                {
                    IDamage temp = item as IDamage;
                    temp.TakeDamage(currentGunStats.damage);
                    break;
                }
            }
        }

        currentBullets--;
        nextTimeToFire = Time.time + 1f / currentGunStats.fireRate;
    }

    public IEnumerator Reloadtime()
    {
        Debug.Log("Hit Reload");
        if (!isReloading && currentBullets != currentGunStats.maxBullets && currentStoredAmmo != 0)
        {
            isReloading = true;
            Debug.Log("Reloading");

            yield return new WaitForSeconds(currentGunStats.reloadTime);

            Debug.Log("Done Reloading");

            int bulletsToReload = Mathf.Min(currentGunStats.maxBullets - currentBullets, currentStoredAmmo);
            currentBullets += bulletsToReload;
            currentStoredAmmo -= bulletsToReload;

            isReloading = false;
        }
        
    }

    public void Reload()
    {
        StartCoroutine(Reloadtime());
    }

    public void Drop()
    {
        //equipped = false;
        // Enable Rigidbody and Collider components
        rb.isKinematic = false;
        coll.enabled = true;

        // Disable shooting and reloading functionality
        enabled = false;

        // Detach from parent (gun container)
        transform.parent = null;
    }
    
    

    public int GetCurrentAmmo()
    {
        return currentBullets;
    }

    public int GetCurrentStoredAmmo()
    {
        return currentStoredAmmo;
    }

    public void RestoreAmmo(int amount)
    {
        currentBullets = Mathf.Min(currentBullets + amount, currentGunStats.maxBullets);
    }

    void PlayAudio()
    {
        if (shootAudio != null)
        {
            audioSource.clip = shootAudio;
            audioSource.Play();
        }
        else
        {
            Debug.Log("AudioClip not assigned!");
        }
    }
}


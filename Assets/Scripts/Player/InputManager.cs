using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] WeaponSwitcher weaponSwitcher;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CameraLook mouseLook;
    [SerializeField] PlayerPause playerPause;
    //[SerializeField] WeaponPickup weaponpickup;
    [SerializeField] Player player;
    [SerializeField] public GunClass equippedGun;

    public static InputManager instance;

    public InputMaster inputMaster;
    InputMaster.MovementActions movement;
    InputMaster.CameraLookActions cameraLook;
    InputMaster.PauseOptionsActions pause;
    public InputMaster.WeaponsActions weapon;

    Vector2 horizontalInput;
    Vector2 mouseInput;
    public void Awake()
    {
        instance = this; // singleton Pattern

        inputMaster = new InputMaster();
        movement = inputMaster.Movement;
        cameraLook = inputMaster.CameraLook;
        pause = inputMaster.PauseOptions;
        weapon = inputMaster.Weapons;

        equippedGun = GetComponentInChildren<GunClass>();

        movement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        movement.Sprint.performed += _ => playerMovement.OnSprintPressed();

        movement.Jump.performed += _ => playerMovement.OnJumpPressed();

        cameraLook.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        cameraLook.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        weapon.Shoot.performed += _ => equippedGun.Shoot();
        weapon.Reload.performed += _ => equippedGun.Reload();
        pause.Pause.performed += _ => playerPause.OnPausePressed();
        weapon.DropWeapon.performed += _ => player.DropGun();   
        weapon.PickupWeapon.performed += _ => player.EquipGun(equippedGun);

       

    }

    

    

    private void Update()
    {
       
        playerMovement.ReceiveInput(horizontalInput);
        if(playerPause.isPaused == false)
        {
            mouseLook.ReceiveInput(mouseInput);
        }
        else
        {
            return;
        }

       
       
        
        
    }

    public void UpdateEquippedGun(GunClass newGun)
    {
        equippedGun = newGun;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLook : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    //Head bobbing stuff
    [SerializeField] float headBobFrequency = 2f;
    [SerializeField] float headBobAmplitude = 0.1f;

    [SerializeField] float lateralBobFrequency = 5f; // Adjust the frequency of lateral head bobbing while sprinting
    [SerializeField] float lateralBobAmplitude = 0.05f;

    float headBobOffset = 2f;

    [SerializeField] Slider sensitivityXSlider;
    [SerializeField] Slider sensitivityYSlider;

    Vector3 originalCameraPosition;

    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalCameraPosition = playerCamera.localPosition;
        GameObject pausedPanel = GameObject.Find("Canvas/PausedPanel");

       if (pausedPanel != null)
        {
            // Find the sliders by type within the paused panel
            sensitivityXSlider = pausedPanel.GetComponentInChildren<Slider>();
            sensitivityYSlider = pausedPanel.GetComponentsInChildren<Slider>()[1]; // Assuming it's the second Slider in the hierarchy

            // Add listeners to the sliders
            sensitivityXSlider.onValueChanged.AddListener(UpdateSensitivityX);
            sensitivityYSlider.onValueChanged.AddListener(UpdateSensitivityY);
        }

        
    }
    private void Update()
    {
        HandleHeadBobbing();


        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }
    public void ReceiveInput (Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }

    private void HandleHeadBobbing()
    {
       

        // Calculate lateral bobbing motion based on sprinting status
        if (playerMovement.isGrounded == true)
        { 
            float bobAmount = Mathf.Sin(Time.time * headBobFrequency) * headBobAmplitude;
            float lateralBobAmount = playerMovement.sprint ? Mathf.Sin(Time.time * lateralBobFrequency) * lateralBobAmplitude : 0f;
            //Calculate the target head bobbing position
            float targetHeadBobPosition = originalCameraPosition.y + bobAmount;

            // Smoothly interpolate to the target head bobbing position
            float currentHeadBobPosition = Mathf.Lerp(playerCamera.localPosition.y, targetHeadBobPosition, Time.deltaTime * 5f);
            // Update the camera's local position with head bobbing
            playerCamera.localPosition = new Vector3(lateralBobAmount, currentHeadBobPosition, 0f);
        }


        

        
    }
    // Method to update sensitivityX based on UI Slider value
    public void UpdateSensitivityX(float value)
    {
        sensitivityX = value;
    }

    // Method to update sensitivityY based on UI Slider value
    public void UpdateSensitivityY(float value)
    {
        sensitivityY = value;
    }









}

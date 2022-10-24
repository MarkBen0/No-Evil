using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float maxHealth = 100f;
    private float Health;
    public UIBar healthBar;
    public float maxStamina = 100f;
    public float Stamina;
    private float staminaUseMultiplier = 5f;
    private float timeBeforeStaminaRegenStarts = 5f;
    private float timeBeforeStaminaBarVanishes = 2f;
    private float staminaValueIncrement = 2f;
    private float staminaTimeIncrement = 0.1f;
    private Coroutine regeneratingStamina;
    private bool isRunning = false;
    private bool canRun = true;
    private float curSpeedX;
    private float curSpeedY;
    public UIBar staminaBar;
    public bool isImgOn;
    public Image img;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
       
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        img.enabled = true;
        isImgOn = true;
        Health = maxHealth;
        healthBar.SetMax(Health);
        Stamina = maxStamina;
        staminaBar.SetMax(Stamina);
        staminaBar.isVisible(false);
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        isRunning = Input.GetKey(KeyCode.LeftShift);
        curSpeedX = canMove ? (canRun && isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        curSpeedY = canMove ? (canRun && isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
           
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
       
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            HandleStamina();
        }
        //Health and Stamina
    }
    private void HandleStamina()
    {
        if (isRunning && ((curSpeedX + curSpeedY) != 0))
        {
            staminaBar.isVisible(true);
            if (regeneratingStamina != null)
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }

            Stamina -= staminaUseMultiplier * Time.deltaTime;

            if (Stamina < 0)
                Stamina = 0;
            staminaBar.SetValue(Stamina);
            if (Stamina <= 0)
                canRun = false;
        }
        if ((!isRunning || ((curSpeedX + curSpeedY) != 0)) && Stamina < maxStamina && regeneratingStamina == null)
        {
            regeneratingStamina = StartCoroutine(RegenerateStamina());
        }
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(timeBeforeStaminaRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);
        while (Stamina < maxStamina)
        {
            if (Stamina > 0)
                canRun = true;

            Stamina += staminaValueIncrement;

            if (Stamina > maxStamina)
                Stamina = maxStamina;

            staminaBar.SetValue(Stamina);

            yield return timeToWait;
        }
        yield return new WaitForSeconds(timeBeforeStaminaBarVanishes);
        staminaBar.isVisible(false);
        regeneratingStamina = null;
    }
}
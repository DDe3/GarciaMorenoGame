using System;
using System.Collections;
using UnityEngine;

public class PlayerControler : MonoBehaviour

{
    public bool CanMove { get; set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && characterController.isGrounded;
    private bool shouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded && !isCrounching;

    [Header("Opciones Funcionales")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadBob = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootSteps = true;

    [Header("Movimiento de camara")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Controles")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.C;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Agacharse")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 1.8f;
    [SerializeField] private float timeCrouch = 0.05f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrounching;

    [Header("Parametros de Salto")]
    [SerializeField] private float gravity = 1f;
    private bool isInAir = false;

    [Header("Parametros de Movimiento")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;


    [Header("Limites y Velocidad de camara")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 30.0f;


    [Header("Interaccion con objetos")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable current;


    [Header("Sonidos")]
    [SerializeField] private float baseSteepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;

    [SerializeField] private AudioSource playerAudioSource = default;
    [SerializeField] private AudioClip[] concreteClips = default;
    [SerializeField] private AudioClip[] tierraClips = default;
    [SerializeField] private AudioClip landingClip = default;
    private float footStepTimer = 0;
    private float GetCurrentOffset => isCrounching ? baseSteepSpeed * crouchStepMultiplier : isSprinting ? baseSteepSpeed * sprintStepMultiplier : baseSteepSpeed;


    [Header("Animacion")]
    [SerializeField] private Animator myAnimator = default;
    [SerializeField] private GameObject modelo = default;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;
    


    private float rotationX = 0;

    
    private float velocity => isCrounching ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed;



    public static PlayerControler instance;

    private void Awake()
    {
        instance = this;
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.visible = false;
    }

    // Update is called once per frame

    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();


            if (canCrouch)
            {
                HandleCrouch();
            }

            if (canUseHeadBob)
            {
                HandleHeadBob();
            }

            if (useFootSteps)
            {
                handleFootsteeps();
            }

            if (canInteract)
            {
                HandleInteractCheck();
                HandleInteractInput();
            }


            ApplyFinalMovement();
        }



    }

    private void handleFootsteeps()
    {
        if (!characterController.isGrounded) return;
        if (currentInput == Vector2.zero) return;
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0)
        {
            if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "Piso/CONCRETO":
                        playerAudioSource.PlayOneShot(concreteClips[UnityEngine.Random.Range(0, concreteClips.Length - 1)]);
                        break;
                    case "Piso/TIERRA":
                        playerAudioSource.PlayOneShot(tierraClips[UnityEngine.Random.Range(0, tierraClips.Length - 1)]);
                        break;
                    default:
                        playerAudioSource.PlayOneShot(concreteClips[UnityEngine.Random.Range(0, concreteClips.Length - 1)]);
                        break;
                }
            }
            footStepTimer = GetCurrentOffset;
        }
    }

    private void HandleMovementInput()
    {
        
        currentInput = new Vector2((velocity) * Input.GetAxis("Vertical"), (isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        myAnimator.SetFloat("velocity", moveDirection.magnitude);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void ApplyFinalMovement()
    {
        if (!characterController.isGrounded)
        {
            isInAir = true;
            myAnimator.SetBool("isInAir", isInAir);
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        if (characterController.isGrounded && isInAir)
        {
            isInAir = false;
            playerAudioSource.PlayOneShot(landingClip);
            myAnimator.SetBool("isInAir", isInAir);
        }
    }


    private void HandleCrouch()
    {
        if (shouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }

    }

    private IEnumerator CrouchStand()
    {
        //No se puede parar si algo esta 1 unidad encima del modelo 
        if (isCrounching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
        {
            yield break;
        }
        //Logica de agacharse
        float targetHeight = isCrounching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrounching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;
        
        // Mover el modelo en Y lo que el Character Controller baja que es 0.94 (depende del modelo)
        float up = isCrounching ? modelo.transform.position.y - 0.94f : modelo.transform.position.y + 0.94f;
        Vector3 pos = new Vector3(characterController.transform.position.x, up ,characterController.transform.position.z);
        modelo.transform.position = pos;
        characterController.height = targetHeight;
        characterController.center = targetCenter;
        isCrounching = !isCrounching;
        //Animacion
        myAnimator.SetBool("isCrouching", isCrounching);
    }

    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) return;
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrounching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrounching ? crouchBobAmount : isSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z
            );
        }
    }


    private void HandleInteractCheck()
    {

        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            if (hit.collider.gameObject.layer == 6 && (current == null || hit.collider.gameObject.GetInstanceID() != current.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out current);
                if (current != null) { current.onFocus(); }
            }
        }
        else if (current)
        {
            current.onLoseFocus();
            current = null;
        }
    }

    private void HandleInteractInput()
    {
        if (Input.GetKeyDown(interactKey) && current != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            current.onInteract();
        }
    }
}

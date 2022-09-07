using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;
    public float runSpeed = 10f;
    private Player playerInput;
    private Transform cameraMain;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform child;
    public CharacterController playerHeight;
    public float normalHeihgt, crounchHeight;
    [HideInInspector] public bool isCrounched, isRun;
    public GameObject standUpInfoUi, crounchInfoUi, standBtnUi, crounchBtnUi, runBtnUi, runInfoUi, walkBtnUi, jumpBtnUi;
    [HideInInspector] public StaminaController _StaminaController;
    [HideInInspector] public float delay = 3f;
    [HideInInspector] public float threshold = .01f;
    [HideInInspector] public bool isNotMove = false;


    private void Awake() {
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
      
    }
   private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }
    private void Start()
    {
        _StaminaController = GetComponent<StaminaController>();
        cameraMain = Camera.main.transform;
        child = transform.GetChild(0).transform;
        isCrounched = false;
        isRun = false;

       
    }
    public void SetRunSpeed(float speed) {
        runSpeed = speed;
    }
    public void PlayerJump() {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movemantInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = (cameraMain.forward * movemantInput.y + cameraMain.right * movemantInput.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        
        
        // Changes the height position of the player..
        if (playerInput.PlayerMain.Jump.triggered && groundedPlayer)
        {
            if(isCrounched){
            _StaminaController.weAreSprinting = false;
            playerHeight.height = normalHeihgt;
            playerSpeed = 5f;
            isCrounched = false; 
            Stand();
            return;
            }
            _StaminaController.weAreSprinting = true;
            _StaminaController.StaminaJump();
        }
        
        if (playerInput.PlayerMain.Crounch.triggered && groundedPlayer)
        {
            _StaminaController.weAreSprinting = false;
            //check player height
            if (!isCrounched) {             
                playerHeight.height = crounchHeight;
                playerSpeed = 2.5f;
                isCrounched = true; 
                isRun = false;
                Crounch();
               return;
            }
            if (isCrounched) {
                playerHeight.height = normalHeihgt;
                isCrounched = false;
                playerSpeed = 5f;
                Stand();
                return;
            }
        }
        if(!isRun) {
            _StaminaController.weAreSprinting = false;   
        }
        
        if(isRun && controller.velocity.sqrMagnitude <= 0 ) {
            Invoke("NotMoving", 3);
        }
        if(isRun && controller.velocity.sqrMagnitude > 0) {
            _StaminaController.weAreNotMoving = false;
            if (_StaminaController.playerStamina > 0) {
                _StaminaController.weAreSprinting = true;
                _StaminaController.Sprinting();
            } else {
                isRun = false;
            }
        }
        if(!isCrounched) {
        if (playerInput.PlayerMain.Run.triggered && groundedPlayer) {
            if(!isRun) {
            _StaminaController.weAreSprinting = true;
            playerSpeed = runSpeed;
            isRun = true;
            isCrounched = false;
            Run();
            return;
            }
            if(isRun) {
            _StaminaController.weAreSprinting = false;
            playerSpeed = 5;
            isRun = false; 
            Stand();
            return;
                }
            }
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if ( movemantInput != Vector2.zero ) {
            Quaternion rotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
    public void Crounch() {
        runBtnUi.SetActive(false);
        standUpInfoUi.SetActive(false);
        walkBtnUi.SetActive(false);
        standBtnUi.SetActive(true);
        crounchInfoUi.SetActive(true);
        runInfoUi.SetActive(false);
        crounchBtnUi.SetActive(false);  
       
    }
    public void Stand() {
        runBtnUi.SetActive(true);
        walkBtnUi.SetActive(false);
        standUpInfoUi.SetActive(true);
        crounchBtnUi.SetActive(true);
        crounchInfoUi.SetActive(false);
        runInfoUi.SetActive(false);
        standBtnUi.SetActive(false);
    }
    public void Run() {
        crounchBtnUi.SetActive(true);
        crounchInfoUi.SetActive(false);
        runInfoUi.SetActive(true);
        walkBtnUi.SetActive(true);
        standBtnUi.SetActive(false);
        standUpInfoUi.SetActive(false);
    }
    public void EmptyStamina() {
        jumpBtnUi.SetActive(false);
        runBtnUi.SetActive(false);
        walkBtnUi.SetActive(false);
        runInfoUi.SetActive(false);
        standUpInfoUi.SetActive(true);
        crounchBtnUi.SetActive(false);
    }
    public void FullStamina() {
        jumpBtnUi.SetActive(true);
       if (isRun) {
           crounchBtnUi.SetActive(true);
           crounchInfoUi.SetActive(false);
           standBtnUi.SetActive(false);
           standUpInfoUi.SetActive(false);
           runInfoUi.SetActive(true);
           runBtnUi.SetActive(false);
           walkBtnUi.SetActive(true);
       }
        if (isCrounched) {
            crounchBtnUi.SetActive(false);
            crounchInfoUi.SetActive(true);
            standBtnUi.SetActive(true);
            standUpInfoUi.SetActive(false);
            runBtnUi.SetActive(false);
        } 
        if (!isCrounched) {
            crounchBtnUi.SetActive(true);
            crounchInfoUi.SetActive(false);
            standBtnUi.SetActive(false);
            standUpInfoUi.SetActive(true);
            runBtnUi.SetActive(true);
        }
       

    }
    public void NotMoving() {
        _StaminaController.weAreNotMoving = true;
        CancelInvoke();
       
    }
   
    
    
}

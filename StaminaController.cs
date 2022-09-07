using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpCost = 20;
    [HideInInspector] public bool hasRegerated =true;
    [HideInInspector] public bool weAreSprinting = false;
    [HideInInspector] public bool weAreNotMoving = false;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)] [SerializeField] private float staminaDrain = 0.5f;
    [Range(0, 50)] [SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina Speed Parameters")]
    [SerializeField] private int slowedRunSpeed = 4;
    [SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina Ui Element")] 
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup slideCanvasGroup = null;

    private PlayerController playerController;

    private void Start() {
        playerController = GetComponent<PlayerController>();

    }

    private void Update() {
        if (!weAreSprinting) {
            if(playerStamina != 0 && playerStamina <= maxStamina - 0.01) {
                playerStamina += staminaRegen * Time.deltaTime;
                playerController.playerSpeed = 5;
                playerController.isRun = false;
                UpdateStamina(1);
            }
            if (playerStamina <= 0  || playerStamina <=1 || playerStamina <= 2) {
                playerStamina += staminaRegen * Time.deltaTime;
                playerController.isRun = false;
                playerController.playerSpeed = 5;
                playerController.EmptyStamina();
                UpdateStamina(1); 
            }
             if (playerStamina >= 98) {
                playerController.SetRunSpeed(normalRunSpeed);
                slideCanvasGroup.alpha = 0;
                hasRegerated = true; 
                playerController.FullStamina();
            }
             if (playerStamina >= 99) {
                playerController.SetRunSpeed(normalRunSpeed);
                slideCanvasGroup.alpha = 0;
                hasRegerated = true; 
                playerController.FullStamina();
            }
             if (playerStamina >= maxStamina) {
                playerController.SetRunSpeed(normalRunSpeed);
                slideCanvasGroup.alpha = 0;
                hasRegerated = true; 
                playerController.FullStamina();
            }
        }
        if (weAreNotMoving) {
            if( playerStamina <= maxStamina - 0.01) {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina(1); 
               
            }
            if ( playerStamina >= 99 ) {
                slideCanvasGroup.alpha = 0;
                hasRegerated = true; 
                 
            }
            if ( playerStamina >= 98 ) {
                slideCanvasGroup.alpha = 0;
                hasRegerated = true; 
                
            }
            if ( playerStamina >= maxStamina ) {
                slideCanvasGroup.alpha = 0;
                hasRegerated = true; 
            }
        }
    }

    public void Sprinting() {
        if(hasRegerated) {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);
            if (playerStamina <= 0) {
                hasRegerated = false;
                playerController.SetRunSpeed(slowedRunSpeed);
                playerController.Stand();
                playerController.isRun = false;
                slideCanvasGroup.alpha = 0;
            }
        }
    }

    public void StaminaJump() {
        if (playerStamina >= (maxStamina * jumpCost / maxStamina)) {
            playerStamina -= jumpCost;
            playerController.PlayerJump();
            UpdateStamina(1);
        }
    }
    

    void UpdateStamina(int value) {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

        if(value == 0) {
            slideCanvasGroup.alpha = 0;
        } else {
            slideCanvasGroup.alpha = 1;
        }
    }
   
}

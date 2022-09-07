using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private Player playerInput;
    public float interactionDistance;
    public GameObject InteractBtn;
    public TMPro.TextMeshProUGUI interactionText;  
    Camera cam;
    // Start is called before the first frame update
    private void Awake() {
        playerInput = new Player(); 
    }
    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }
    void Start()
    {
        cam = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;

        bool successfulHit = false;

        if(Physics.Raycast(ray, out hit, interactionDistance)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
             if(hit.transform.tag == "LightSwitch") {
                if (interactable !=null ) {
                    InteractBtn.SetActive(true);
                    HandleInteraction(interactable);
                    interactionText.text = interactable.GetDescription();
                    successfulHit = true;
                }
             }
             else if(hit.transform.tag == "Door") {
                if (interactable !=null ) {
                    InteractBtn.SetActive(true);
                    HandleInteraction(interactable);
                    interactionText.text = interactable.GetDescription();
                    successfulHit = true;
                }
             }
        }
 
        if (!successfulHit) {
            InteractBtn.SetActive(false);
            interactionText.text = "";
        }
    }

    void HandleInteraction(Interactable interactable) {
        
        if (playerInput.PlayerMain.Interact.triggered) {
        Debug.Log("HII");
        interactable.Interact(); 
        }
    }
}

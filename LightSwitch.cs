using UnityEngine;

public class LightSwitch : Interactable
{
   public Light m_Light;
   public bool isOn;
   public GameObject colorEm;

   private void Start() {
      UpdateLight();
   }

   void UpdateLight() {
     m_Light.enabled = isOn;
     if (isOn) {
          colorEm.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
          colorEm.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
     } else {
          colorEm.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
          colorEm.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
     }
   }

   public override string GetDescription() {
     if (isOn) return "Press [E] to turn <color=red>off</color> the light.";
               return "Press [E] to turn <color=green>on</color> the light.";
   }
   public override void Interact() { 
     
     isOn = !isOn;
     UpdateLight();
   }
}

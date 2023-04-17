using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMeter : MonoBehaviour
{
    private void OnMouseOver(){
        string message = "This is Smart meter. It can reflect the current electricity consumption required for your production. Please note that, just like in real life, we produce electricity based on demand. In other words, at the beginning of each turn, if any excess electricity is not stored, it will be automatically consumed.";

        InformationManager.Instance.showInInformationBox(message,true);
   }
}

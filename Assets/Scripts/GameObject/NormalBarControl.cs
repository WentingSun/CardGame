using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBarControl : MonoBehaviour
{

    public string barName;
    public Slider Bar;
    public float maxValue;
    public float minValue;
    [SerializeField] float currentValue;

    public void Enable(bool SetActive){
        Bar.gameObject.SetActive(SetActive);
    }

    public void resetBar(){
        currentValue = 0;
        Bar.value = 0;
    }

    public void changeCurrentBarValue(float changeValue){
        currentValue += changeValue;
        Bar.value = currentValue;
        if(currentValue == maxValue){
            ResourceManager.Instance.handleBar(this,true);
        }else if(currentValue == minValue){
            ResourceManager.Instance.handleBar(this,false);
        }
    }

    private void Awake() {
        barName = this.gameObject.name;
        maxValue = Bar.maxValue;
        minValue = Bar.minValue;
    }

    private void OnMouseOver(){
        string message = "";
        switch(name){
            case "NatureBar":
            message = $"This is NatureBar. This is a indicator represents the environment. \n\nMax value is {maxValue}. Min value is {minValue}. Current Value is {Bar.value}. \n\nAt the beginning of each round, the current value will increase by 15 points due to the self-healing ability of nature. ";
            break;
            case "HumanitiesBar":
            message = $"This is HumanitiesBar. This is similar to HDI. \n\nMax value is {maxValue}. Min value is {minValue}. Current Value is {Bar.value}. \n\nAt the beginning of each round, the current value will increase based on the number of consecutive rounds that nobody is working in harsh conditions.";
            break;

        }

        InformationManager.Instance.showInInformationBox(message,true);
   }


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

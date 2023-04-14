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


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBarControl : MonoBehaviour
{

    public string barName;
    public Slider Bar;
    public float maxValue;
    [SerializeField] float currentValue;

    public void Enable(bool SetActive){
        Bar.gameObject.SetActive(SetActive);
    }

    public void changeCurrentBarValue(float changeValue){
        currentValue += changeValue;
        Bar.value = currentValue;
        checkBarState();
    }

    private void Awake() {
        barName = this.gameObject.name;
    }

    private void checkBarState(){//todo

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

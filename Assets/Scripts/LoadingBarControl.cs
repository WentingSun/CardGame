using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider loadingBar;
    public float maxTime;
    private float timeLeft;

    public void Enable(bool SetActive){
        loadingBar.gameObject.SetActive(SetActive);
    }

    public void processingLoad(float duration){
        Enable(true);
        maxTime = duration;
        timeLeft = duration;
        loadingBar.maxValue = maxTime;
        loadingBar.value = timeLeft;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0){
            timeLeft -= Time.deltaTime;
            loadingBar.value = timeLeft;
        }
    }
}

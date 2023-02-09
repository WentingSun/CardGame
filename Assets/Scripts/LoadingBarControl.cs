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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

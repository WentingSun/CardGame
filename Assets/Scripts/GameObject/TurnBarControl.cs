using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurnBarControl : MonoBehaviour
{
    public Slider TurnBar;
    public float maxTime;
    private float timeLeft;


    private void Awake() {
        maxTime = TurnBar.maxValue;
        timeLeft = maxTime;
        TurnBar.value = timeLeft;
    }

    public void setTheTurnTime(float time){
        maxTime = time;
        TurnBar.maxValue = time;
        timeLeft = time;
        TurnBar.value = timeLeft;
    }

    void Update()
    {
        if(timeLeft > 0){
            
            if(GameManager.Instance.State == GameState.PlayerTurn){
                timeLeft -= Time.deltaTime;
            }
            TurnBar.value = timeLeft;
            // Debug.Log(timeLeft);
        }else{
            GameManager.Instance.UpdateGameState(GameState.Settlement);
        }
    }

}

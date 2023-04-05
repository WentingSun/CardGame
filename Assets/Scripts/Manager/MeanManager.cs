using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanManager : Singleton<MeanManager>
{

    public void activiteMenu(){
        
    }

    protected override void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += MeanManagerOnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= MeanManagerOnGameStateChanged;
    }

    void MeanManagerOnGameStateChanged(GameState newState){

    }

}

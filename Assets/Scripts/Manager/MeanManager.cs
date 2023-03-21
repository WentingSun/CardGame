using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanManager : Singleton<MeanManager>
{

    override protected void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

    void GameManagerOnGameStateChanged(GameState newState){

    }

}

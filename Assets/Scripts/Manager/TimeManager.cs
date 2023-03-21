using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    protected override void Awake() {
        base.Awake();
        GameManager.OnGameStateChange += TimeManagerOnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= TimeManagerOnGameStateChanged;
    }

    void TimeManagerOnGameStateChanged(GameState newState){

    }
}

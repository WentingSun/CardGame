using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuControl : MonoBehaviour
{
    public void RetryButton(){
        GameManager.Instance.initTheGame();
        MenuManager.Instance.GameOverMenu.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Start);
    }


    public void QuitButton(){
        GameManager.Instance.QuitTheGame();
        MenuManager.Instance.GameOverMenu.SetActive(false);
    }

}

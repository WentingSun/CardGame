using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControl : MonoBehaviour
{
    public void PlayGame(){
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        SceneManager.LoadScene(2,LoadSceneMode.Additive);
        
    }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.buildIndex == 2)
        {

            GameManager.Instance.initTheGame();


            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void Quit(){
        Debug.Log("Quit Game.");
        Application.Quit();
    }
}

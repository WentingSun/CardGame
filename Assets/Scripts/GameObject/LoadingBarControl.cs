using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadingBarControl : MonoBehaviour
{

    public Slider loadingBar;
    public float maxTime;
    private float timeLeft;

    public bool isProcessing;

    private Stack taskStack;

    private List<StackTask> stackTasks;
    private Action<StackTask> TaskAction;

    public void Enable(bool SetActive){
        loadingBar.gameObject.SetActive(SetActive);
    }

    public void processingLoad(float duration, Stack stack){
        Enable(true);
        maxTime = duration;
        timeLeft = duration;
        loadingBar.maxValue = maxTime;
        loadingBar.value = timeLeft;
        taskStack = stack;
    }

    public void processingLoad( List<StackTask> Tasks, Action<StackTask> action){
        if(Tasks[0].taskValue>0){
            Enable(true);
        }
        var duration = Tasks[0].taskValue;
        maxTime = duration;
        timeLeft = duration;
        loadingBar.maxValue = maxTime;
        loadingBar.value = timeLeft;
        stackTasks =Tasks;
        TaskAction = action;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0){
            isProcessing = true;
            if(GameManager.Instance.State == GameState.PlayerTurn){
                timeLeft -= Time.deltaTime;
            }
            loadingBar.value = timeLeft;
        }else if(stackTasks?.Count >= 0 && TaskAction!= null){
            foreach(StackTask task in stackTasks){
                TaskAction(task);
            }
            isProcessing = false;
        }
    }
}

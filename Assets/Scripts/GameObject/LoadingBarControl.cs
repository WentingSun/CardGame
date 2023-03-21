using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadingBarControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider loadingBar;
    public float maxTime;
    private float timeLeft;

    private Stack taskStack;

    private List<StackTask> stackTasks;
    private Action<StackTask> TaskAction;

    public void Enable(bool SetActive){
        loadingBar.gameObject.SetActive(SetActive);
    }

    public void processingLoad(float duration, Stack stack){//TODO 
        Enable(true);
        maxTime = duration;
        timeLeft = duration;
        loadingBar.maxValue = maxTime;
        loadingBar.value = timeLeft;
        taskStack = stack;
    }

    public void processingLoad( List<StackTask> Tasks, Action<StackTask> action){
        Enable(true);
        var duration = Tasks[0].taskTime;
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
            timeLeft -= Time.deltaTime;
            loadingBar.value = timeLeft;
            // Debug.Log(timeLeft);
        }else if(taskStack != null){
            Enable(false);
            taskStack.processingStack();
            // taskStack.updateCardList();
        }else if(stackTasks?.Count >= 0 && TaskAction!= null){
            foreach(StackTask task in stackTasks){
                TaskAction(task);
            }
            TaskAction = null;
        }
    }
}

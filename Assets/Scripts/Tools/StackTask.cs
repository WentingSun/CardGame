
public enum TaskType{
    Create,
    Change,
    Destroy,
    ChangeBarValue,
    Idle
}

public enum typeBar{
    NaturalBar,
    HumanitiesBar,
    Null
}


public struct StackTask {
        public TaskType taskType {get;}
        public int  taskIndex {get;}
        public float taskValue {get;}

        // public typeBar taskBar{get;}

        // public float taskBarChangeValue{get;}


        
        
        // public StackTask(TaskType _taskType, int  _taskIndex, float _taskTime, typeBar _taskBar, float _taskBarChangeValue){
            public StackTask(TaskType _taskType, int  _taskIndex, float _taskValue){
            taskIndex = _taskIndex;
            taskType = _taskType;
            taskValue = _taskValue;
            // taskBar = _taskBar;
            // taskBarChangeValue =_taskBarChangeValue;
        }

    }

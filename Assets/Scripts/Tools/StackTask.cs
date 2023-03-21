
public enum TaskType{
    Create,
    Change,
    Destroy,
    Idle
}


public struct StackTask {
        public TaskType taskType {get;}
        public int  taskIndex {get;}
        public float taskTime {get;}
        
        public StackTask(TaskType _taskType, int  _taskIndex, float _taskTime){
            taskIndex = _taskIndex;
            taskType = _taskType;
            taskTime = _taskTime;
        }

    }

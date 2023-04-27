
public enum TaskType{
    Create,
    Change,
    Destroy,
    ChangeBarValue,
    ChangeCardValue,
    ChangePlayBuff,
    IdealSolutionCard,
    Idle
}

public struct StackTask {
        public TaskType taskType {get;}
        public int  taskIndex {get;}
        public float taskValue {get;}

            public StackTask(TaskType _taskType, int  _taskIndex, float _taskValue){
            taskIndex = _taskIndex;
            taskType = _taskType;
            taskValue = _taskValue;
        }

    }

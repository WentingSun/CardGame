using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{


    [SerializeField] List<Card> stackedCards;
    [SerializeField] List<int> cardIds;

    
    [SerializeField] StackTask currentTask;

    public CardData[] cardDatas;

    // [SerializeField] GameManager gameManager;
    
    public void addCard(Card card)
    {
        stackedCards.Add(card);
    }

    public void removeCard(Card card)
    {
        stackedCards.Remove(card);
    }

    public void updateCardList(){
        stackedCards.Clear();

        for(int i =0 ; i < this.transform.childCount ; i++){
            stackedCards.Add(transform.GetChild(i).GetComponent<Card>());
        }
    }

    public void disableCollider(){
        for(int i = 0; i < stackedCards.Count ; i++){
            stackedCards[i].gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void enableCollider(){
        for(int i = 0; i < stackedCards.Count ; i++){
            stackedCards[i].gameObject.GetComponent<Collider2D>().enabled = true;
            stackedCards[i].gameObject.transform.position += new Vector3(0,0,-i);
        }
    }

    public void checkStackState(){
        
        updateCardList();
        updateCardIds();
        updateStackState();



    }

    private void updateStackState(){
        for(int i = 0; i < stackedCards.Count; i++){
            if(i == 0 && checkCardCombine()){//TODO add some check, true for processing the stack.
                // processingLoad(stackedCards[i].loadingBar,10);
                // List<StackTask> tasks = checkTheCombine();
                stackedCards[i].loadingBar.processingLoad(currentTask.taskTime,this);//todo action<stack task>
                // stackedCards[i].loadingBar.processingLoad(tasks,processingStack);
                
            }else{
                stackedCards[i].loadingBar.Enable(false);
            }
        }
    }

    private bool checkCardCombine(){//TODO add more check
    if(cardIds.Count > 1){
        if(cardIds[0] == 3 && cardIds[1] == 1){
            currentTask = new StackTask(TaskType.Create, 2,5);
            return true;
            }
            else if (cardIds[0] == 4 && cardIds[1] == 1){
            currentTask = new StackTask(TaskType.Destroy,1,5);
            return true;
            }//TODO
            


    }
        currentTask = new StackTask(TaskType.Idle,-1,10);
        return false;
    }

    // private List<StackTask> checkTheCombine(){
    //     List<StackTask> result = new List<StackTask>();
    //     if(cardIds.Count>1){
    //         if(cardIds[0] == 3 && cardIds[1] == 1){
    //             result.Add(new StackTask(TaskType.Create,2,5));
    //         }
    //     }
    //     return result;
    // }

    public void processingStack(){//TODO all processing stack should done by this function, add task id.
        Debug.Log("ProcessingStack");
        switch(currentTask.taskType){
            case TaskType.Create:
                Debug.Log("TaskType.Create");
                createCard(cardDatas[currentTask.taskIndex]);
                ResourceManager.Instance.changeNaturalBarValue(1f);// TODO should remove
                checkStackState();
            break;

            case TaskType.Change:
                Debug.Log("TaskType.Change");
                checkStackState();
            break;

            case TaskType.Destroy://todo meet bug when uing checkStakeState in destory task
                Debug.Log("TaskType.Destroy");
                destoryCard(currentTask.taskIndex);
                // checkStackState();

            break;
        }
        Debug.Log("Task finished");
        // stackedCards[0].cardData = cardDatas[2];
        // stackedCards[0].loadCardData();
        // updateCardList();
    }

    // public void processingStack(StackTask stackTask){
    //     Debug.Log("ProcessingStack");
    //     switch(stackTask.taskType){
    //         case TaskType.Create:
    //             Debug.Log("TaskType.Create");
    //             createCard(cardDatas[stackTask.taskIndex]);
    //             ResourceManager.Instance.changeNaturalBarValue(1f);
    //             checkStackState();
    //         break;

    //         case TaskType.Change:
    //             Debug.Log("TaskType.Change");
    //             checkStackState();
    //         break;

    //         case TaskType.Destroy://todo meet bug when uing checkStakeState in destory task
    //             Debug.Log("TaskType.Destroy");
    //             destoryCard(stackTask.taskIndex);
    //             // checkStackState();

    //         break;
            
    //     }
    //     Debug.Log("Task finished");

    // }

    private void updateCardIds(){
        cardIds.Clear();
        foreach(Card card in stackedCards){
            this.cardIds.Add(card.getCardId());
        }
        
    }

    public void createCard(CardData cardData){
        GameObject newCard = Instantiate(ResourceManager.Instance.cardTemple, this.transform);
        newCard.GetComponent<Card>().currentStack = this;
        newCard.GetComponent<Card>().cardData = cardData;
        newCard.GetComponent<Card>().loadCardData();
        newCard.transform.position = getLowestPosition() + new Vector3(0,-0.22f,0);
        
    }

    public void destoryCard(int index){
        var theCard = stackedCards[index].gameObject;
        Destroy(theCard);
        cardIds.RemoveAt(index);
        stackedCards.RemoveAt(index);

        // updateCardList();
        // updateCardIds();
        // checkStackState();

        //resetStackedCardsPosition //TODO
        if(GameManager.Instance.State == GameState.PlayerTurn){
            updateStackState();
        }
        
        
        
    }

    private Vector3 getLowestPosition(){
        return stackedCards[stackedCards.Count-1].transform.position;
    }

    public void destroyStack(){
        stackedCards[0].loadingBar.Enable(false);
        Destroy(this.gameObject);
    }

    // private void processingLoad(Slider loadingBar, float duration){
    //     loadingBar.gameObject.SetActive(true);
    //     loadingBar.maxValue = duration;
    //     // bool result = false;
    //     // while(loadingBar.value >0){
    //     //     loadingBar.value -= Time.deltaTime;
    //     // }
    //     // result = true;
    //     // return result;
    // }

    // Start is called before the first frame update
    void Start()
    {
       cardDatas = ResourceManager.Instance.cardDatas;
    //    checkStackState();
    //    foreach(Card card in stackedCards){
    //     card.loadCardData();
    //    }
    }

    // Update is called once per frame
    void Update()
    {
        // updateCardList();
    }
    
    private void OnDestroy() {
        ResourceManager.Instance.removingStackList(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{


    [SerializeField] List<Card> stackedCards;
    [SerializeField] List<int> cardIds;

    public CardData[] cardDatas;

    public Stack targetStack;

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
        resetStackedCardsPosition();



    }

    private void updateStackState(){
        List<StackTask> tasks = checkTheCombine();
        for(int i = 0; i < stackedCards.Count; i++){
            if(i == 0 && tasks.Count > 0){
                stackedCards[i].loadingBar.processingLoad(tasks,processingStack);
            }else{
                stackedCards[i].loadingBar.Enable(false);
            }
        }
    }


            


    private List<StackTask> checkTheCombine(){//TODO add more combine
        // Debug.Log("checkTheCombine()");
        List<StackTask> result = new List<StackTask>();
        if(cardIds.Count>1){
            if(cardIds[0] == 3 && cardIds[1] == 1){
                result.Add(new StackTask(TaskType.Create,2,5));
                result.Add(new StackTask(TaskType.ChangeBarValue,0,1f));
            }else if(cardIds[0] == 4 && cardIds[1] == 1){// TODO testing CODE, you should change it in future
                result.Add(new StackTask(TaskType.Destroy,1,5));
            }
        }else if(cardIds.Count == 1){
            if(cardIds[0] == 6){
                result.Add(new StackTask(TaskType.Create,5,getSolarPlaneTaskTime()));
            }
        }
        return result;
    }

    private int getSolarPlaneTaskTime(){
        int result = 20;
        switch(GameManager.Instance.currentWeatherState){
            case WeatherState.Sunny:
            result = 10;
            break;
            case WeatherState.Rainy:
            result = -1; // -1 means not enable the loading bar
            break;
            default:
            result = 20;
            break;
        }
        return result;
    }


    public void processingStack(StackTask stackTask){
        Debug.Log("ProcessingStack");
        switch(stackTask.taskType){
            case TaskType.Create:
                Debug.Log("TaskType.Create");
                createCard(cardDatas[stackTask.taskIndex]);
                checkStackState();
            break;

            case TaskType.Change:
                Debug.Log("TaskType.Change");
                checkStackState();
            break;

            case TaskType.Destroy://todo meet bug when uing checkStakeState in destory task
                Debug.Log("TaskType.Destroy");
                destoryCard(stackTask.taskIndex);
                // checkStackState();
            break;

            case TaskType.ChangeBarValue:
                Debug.Log("TaskType.ChangeBarValue");
                switch(stackTask.taskIndex){
                    case 0: //NaturalBar
                    ResourceManager.Instance.changeNaturalBarValue(stackTask.taskValue);
                    break;
                    case 1://HumanitiesBar
                     ResourceManager.Instance.changeHumanitiesBarValue(stackTask.taskValue);
                    break; 

                }
                break;

            case TaskType.Idle:
                Debug.Log("Idle");
            break;
            
        }
        InformationManager.Instance.updateSmartMeterInfo();

        Debug.Log("Task finished");

    }

    private void updateCardIds(){
        cardIds.Clear();
        foreach(Card card in stackedCards){
            this.cardIds.Add(card.getCardId());
        }
        
    }

    public void createCard(CardData cardData){// TODO void createCard(CardData cardData, Stack targetStack)
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
        // DO NOT USING ABOVE FUNCTION HERE!!! I dont know why, but the bug is about updating the stack attribute before card destory.
        if(cardIds.Count == 0 ) {
            Destroy(this.gameObject);
        }else{
            resetStackedCardsPosition(); //TODO
            if(GameManager.Instance.State == GameState.PlayerTurn){
            updateStackState();
            }
        }

        
       
        
        
        
    }

    public void resetStackedCardsPosition(){
        if(stackedCards.Count >0 && stackedCards[0] != null ){
            Vector3 initPosition = stackedCards[0].transform.position;
            Vector3 offsets =new Vector3(0,-0.22f,0);
            for(int i = 0 ; i<stackedCards.Count ; i++){
                if(stackedCards[i]!=null){
                    stackedCards[i].transform.position = initPosition + offsets * i;
                }
            }
        }
        
    }

    private Vector3 getLowestPosition(){
        return stackedCards[stackedCards.Count-1].transform.position;
    }

    private void createEmptyStack(){
        GameObject newStack = Instantiate(ResourceManager.Instance.stackTemple,this.transform.parent);
        newStack.transform.position = new Vector3(0,0,0);
        //newStack.transform.position = getRandomNearByPositionOf(this.transform)
        this.targetStack = newStack.GetComponent<Stack>();
    }

    public void destroyStack(){
        if(stackedCards[0] != null){
            stackedCards[0].loadingBar.Enable(false);
        }
        
        Destroy(this.gameObject);
    }


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

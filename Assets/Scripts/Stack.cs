using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] List<Card> stackedCards;
    [SerializeField] List<int> cardIds;

    [SerializeField] GameObject cardTemple;

    public CardData[] cardDatas;
    
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
        checkStackState();
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

    private void checkStackState(){
        updateCardIds();
        for(int i = 0; i < stackedCards.Count; i++){
            if(i == 0 && checkCardCombine()){//TODO add some check, true for processing the stack.
                // processingLoad(stackedCards[i].loadingBar,10);
                stackedCards[i].loadingBar.processingLoad(10,this);
            }else{
                stackedCards[i].loadingBar.Enable(false);
            }
        }
    }

    private bool checkCardCombine(){//TODO add more check
        if(cardIds.Contains(1)){
            return false;//TODO
            }
        return false;
    }

    public void processingStack(){//TODO all processing stack should done by this function, add task id.
        Debug.Log("ProcessingStack");
        createCard(cardDatas[1]);
        // stackedCards[0].cardData = cardDatas[2];
        // stackedCards[0].loadCardData();
        updateCardList();
    }

    private void updateCardIds(){
        cardIds.Clear();
        foreach(Card card in stackedCards){
            this.cardIds.Add(card.getCardId());
        }
        
    }

    public void createCard(CardData cardData){
        GameObject newCard = Instantiate(cardTemple, this.transform);
        newCard.GetComponent<Card>().currentStack = this;//TODO
        newCard.GetComponent<Card>().cardData = cardData;
        newCard.GetComponent<Card>().loadCardData();
        newCard.transform.position = stackedCards[stackedCards.Count-1].transform.position + new Vector3(0,-0.22f,0);
        
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
       cardDatas = Resources.LoadAll<CardData>("CardData");
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
}

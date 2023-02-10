using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] List<Card> stackedCards;
    [SerializeField] List<int> cardIds;
    
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
            if(i == 0 && true){//TODO add some check, true for processing the stack.
                // processingLoad(stackedCards[i].loadingBar,10);
                stackedCards[i].loadingBar.processingLoad(10,this);
            }else{
                stackedCards[i].loadingBar.Enable(false);
            }
        }
    }

    public void processingStack(){//TODO all processing stack should done by this function, add task id.
        Debug.Log("ProcessingStack");

        updateCardList();
    }

    private void updateCardIds(){
        cardIds.Clear();
        foreach(Card card in stackedCards){
            this.cardIds.Add(card.getCardId());
        }
        
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
        
    }

    // Update is called once per frame
    void Update()
    {
        // updateCardList();
    }
}

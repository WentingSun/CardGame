using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public Card Instance;
    public TextMeshProUGUI nameText;
    [SerializeField] Transform parentDuringMove;
    [SerializeField] Stack TargetStack;
    [SerializeField] Collider2D touchedCollider;
    [SerializeField] GameObject stack_Prefabs;
    [SerializeField] GameObject canvas;

    [SerializeField] Stack currentStack;
    [SerializeField] List<Transform> upperCards;

    private Vector3 dragOffset;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    private void AddingUpperCards(){
        upperCards = new List<Transform>();
        for(int i = transform.GetSiblingIndex(); i < currentStack.transform.childCount; i++ ){
            upperCards.Add(currentStack.transform.GetChild(i));
        }
        // Debug.Log("C");
    }
    
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    private void OnMouseDown()
    {
        dragOffset = transform.position - getMousePosition();

        AddingUpperCards();

        if (this.transform == currentStack.transform.GetChild(0)){ //TODO Stack removing function
            foreach(Transform card in upperCards){
                card.SetParent(canvas.transform);
                card.gameObject.GetComponent<Collider2D>().enabled = false;
            }
                this.gameObject.GetComponent<Collider2D>().enabled = true;
            // Debug.Log("D");
            Destroy(currentStack.gameObject);
        }else{
             this.transform.SetParent(canvas.transform);
        }




    }

    
    
    private void OnMouseDrag()
   {
    
    upperCards[0].position = getMousePosition() + dragOffset;
    for(int i = 1; i < upperCards.Count; i++){
        upperCards[i].position = upperCards[i-1].position + new Vector3(0,-0.22f*i,0)  ;
    }

    // transform.position = getMousePosition() + dragOffset;
    // transform.SetSiblingIndex(200);
   }
   
   /// <summary>
   /// OnMouseDown is called when the user has pressed the mouse button while
   /// over the GUIElement or Collider.
   /// </summary>
   private void OnMouseUp()
   {
    if(touchedCollider != null){//TODO Stack adding function
        Instance.transform.position = getLowestPosition(TargetStack.transform) + new Vector3(0,-0.22f,0);
        Instance.transform.SetParent(touchedCollider.transform.parent);
        currentStack = TargetStack;
    }else {
        // Debug.Log("Should create a stack");
        createStack();
    }
    foreach(Transform card in upperCards){
        card.gameObject.GetComponent<Collider2D>().enabled = true;
        card.SetParent(currentStack.transform);
    }
    upperCards = null;
   }

   Vector3 getMousePosition()
   {
    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0;
    return mousePosition;
   }

   /// <summary>
   /// Sent when another object enters a trigger collider attached to this
   /// object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   private void OnTriggerStay2D(Collider2D other)
   {
    touchedCollider = other;
    TargetStack = other.gameObject.transform.parent.GetComponent<Stack>();
   }

   /// <summary>
   /// Sent when another object leaves a trigger collider attached to
   /// this object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   private void OnTriggerExit2D(Collider2D other)
   {
    // Debug.Log("Leaveing");
    touchedCollider = null;
    TargetStack = null;
   }

    // Start is called before the first frame update
    void Start()
    {
        createStack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createStack(){
        GameObject newStack = Instantiate(stack_Prefabs,canvas.transform);
        this.transform.SetParent(newStack.transform);
        currentStack = newStack.GetComponent<Stack>();
    }

    private Vector3 getLowestPosition(Transform targetStack)//TODO getLowestCardTransform
    {
        Debug.Log(targetStack.childCount);
        var result = targetStack.GetChild(targetStack.childCount -1 ).position;
        return result;
    }


}
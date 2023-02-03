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
    [SerializeField] Transform parentBeforeMove;
    [SerializeField] Collider2D touchedCollider;
    [SerializeField] GameObject stack_Prefabs;
    [SerializeField] GameObject canvas;

    private Vector3 dragOffset;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }
    
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    private void OnMouseDown()
    {
        dragOffset = transform.position - getMousePosition();
        if (this.transform == this.transform.parent.GetChild(0)){ 
            GameObject currentStack =this.transform.parent.gameObject;
            this.transform.SetParent(canvas.transform);
            Destroy(currentStack);
        }
    }
    
    private void OnMouseDrag()
   {
    transform.position = getMousePosition() + dragOffset;
    transform.SetSiblingIndex(200);
   }
   
   /// <summary>
   /// OnMouseDown is called when the user has pressed the mouse button while
   /// over the GUIElement or Collider.
   /// </summary>
   private void OnMouseUp()
   {
    if(touchedCollider != null){//TODO
        Instance.transform.position = touchedCollider.transform.position + new Vector3(0,-0.22f,0);
        Instance.transform.SetParent(touchedCollider.transform.parent);
    }else {
        Debug.Log("Should create a stack");
        createStack();
    }
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
   }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createStack(){
        GameObject newStack = Instantiate(stack_Prefabs,canvas.transform);
        this.transform.SetParent(newStack.transform);
    }


}
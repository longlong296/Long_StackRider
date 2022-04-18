using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
   public  Stack myStack =new Stack();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Balls")
        {
            myStack.Push(this.gameObject.name);
            Debug.Log(myStack.Peek());
                
        }
    }
}

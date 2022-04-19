using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] float fowardForce = 5000f;
    //[SerializeField] float sidewayForce = 1500f;
    private Rigidbody rbChar;
    private Transform charr;
    
    public static int score = 0;
    private float limiter;
    private float xPos;

    public static bool going = false;
    public static int buttonNumber = 999;

    public static int ballCount = 1;
    public Stack myStack = new Stack();
    Vector3 speed;

    



    // Start is called before the first frame update
    void Start()
    {
       rbChar = GetComponent<Rigidbody>();
        charr=GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()

    {
     
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
           limiter= hit.collider.gameObject.GetComponentInChildren<Renderer>().bounds.size.x;
        }

        //dy chuyen ve phia truoc
#if UNITY_EDITOR
        if (!going == true) { 
        transform.Translate(new Vector3(0, 0, 2) * Time.deltaTime);
        //rbChar.AddForce(0, 0, fowardForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            {
            transform.Translate(new Vector3(2, 0, 2) * Time.deltaTime);
                
            }

            if (Input.GetKey(KeyCode.A))
            {

            transform.Translate(new Vector3(-2, 0, 2) * Time.deltaTime);
           
            }
        }

#endif


#if UNITY_ANDROID
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                    {
                        float desiredPlayerPos = Mathf.Clamp(touch.deltaPosition.x * 2 + transform.position.x, -6f, 6f);
                        xPos = Mathf.SmoothDamp(transform.position.x, desiredPlayerPos, ref velocity, 3.5f * Time.deltaTime);
                    }
                }
                Vector3 moveVector = new Vector3(xPos - transform.position.x, 0, runSpeed * Time.deltaTime);
                characterController.Move(moveVector);
#endif
        //should be raycasting to check the boundary
        invinsibleBoundary(limiter/2, -(limiter/2));



    }

    void invinsibleBoundary(float a, float _a)
    {

        //raycasting instead of hard adjustment
      
        //chinh boundary qua ben trai
        if (transform.position.x > a - 0.1f)
        {
            transform.position = new Vector3(a - 0.1f, transform.position.y, transform.position.z);
        }

        //chinh boundary qua ben phai
        if (transform.position.x < _a + 0.1f)
        {
            transform.position = new Vector3(_a + 0.1f, transform.position.y, transform.position.z);
        }


    }
    private void OnTriggerEnter(Collider other)
    {

        //if (other.tag == "Balls")
        //{
            
        //    myStack.Push(other.gameObject);
        //    other.transform.parent = this.transform;
        //    Debug.Log(myStack.Peek());
        //}
        if(other.tag == "Coin")
        {
            score++;
            Destroy(other.gameObject);
            Debug.Log("Cong 1 diem");
            Vibration.Vibrate();
        }
        else if (other.tag == "Wall")
        {
            Debug.Log("Lose a ball");

        }
        else if (other.tag == "Finish")
        {

            StartCoroutine(calculationFinish(0.2f));
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Balls")
        {
              myStack.Push(col.gameObject);
              col.gameObject.transform.parent = this.transform;
              Debug.Log(myStack.Peek());
        }
    }

    IEnumerator calculationFinish(float a)
    {
       
        myStack.Pop();
        yield return new WaitForSeconds(a);
    }
}

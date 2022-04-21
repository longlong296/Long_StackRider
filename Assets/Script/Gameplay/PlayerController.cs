using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbChar;
    private Transform charr;

    public static int score = 0;
    private float limiter;
    private float xPos;

    public static bool going = false;
    public static bool winGame = false;
    public static bool lostGame = false;
    public static int buttonNumber = 999;

    public static int ballCount = 1;
    public Stack<GameObject> myStack = new Stack<GameObject>();
    Animator characterAnim;
    Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {   
        characterAnim = GetComponent<Animator>();   
        rbChar = GetComponent<Rigidbody>();
        charr = GetComponent<Transform>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) &&!winGame||!lostGame)
        {

            going = true;
        }
    }
    // Update is called once per frame
    void LateUpdate()

    {
        //raycasty is still expensive
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            limiter = hit.collider.gameObject.GetComponentInChildren<Renderer>().bounds.size.x;
        }

        //dy chuyen ve phia truoc
#if UNITY_EDITOR
        if (!going == false)
        {
            characterAnim.SetBool("going",true);

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
        }

        //should be raycasting to check the boundary
        invinsibleBoundary(limiter / 2, -(limiter / 2));
    }

    void invinsibleBoundary(float a, float _a)
    {
        //raycasting instead of hard adjustment
        //chinh qua ben phai
        if (transform.position.x > a - 0.1f)
        {
            transform.position = new Vector3(a - 0.1f, transform.position.y, transform.position.z);
        }
        //chinh boundary qua ben trai
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
         if (other.tag == "Coin")
        {
            score++;
            Destroy(other.gameObject);
            Debug.Log("Cong 1 diem");
            Vibration.Vibrate();
        }
        //else if (other.tag == "Wall")
        //{
        //    Debug.Log("Lose a ball");
        //    ballCount--;
        //    //Debug.Log(ballCount);
        //    if (ballCount != 0)
        //    {
        //        //wall logic
        //    }
        //    else
        //    {
        //        Debug.Log("Lose game");
        //        //GameObject.Find("LoseGame").SetActive(true);
        //        this.transform.Translate(Vector3.zero);
        //        going = false;
        //        lostGame = true;
        //        //lose game logic
        //    }
        //}

        else if (other.tag == "Finish")
        {
            going = false;
            //cho vao giua dich
            //Vector3.Lerp(transform.position,new Vector3( other.transform.position.x, transform.position.y,other.transform.position.z),0.3f);
            //stop
            //this.transform.Translate(Vector3.zero);
            StartCoroutine(calculationFinish(0.5f));

            if (winGame == true) { StartCoroutine(waitWin()); }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Balls")
        {
            ballCount++;
            

            if (ballCount > 2)
            {
                Vector3 a = new Vector3(transform.localPosition.x, myStack.Peek().gameObject.transform.localPosition.y - 1, transform.localPosition.z);
               pushBall(col.gameObject, a);    
            }
            else
            {
                Vector3 _a = new Vector3(transform.localPosition.x, transform.localPosition.y-1, transform.localPosition.z);
                pushBall(col.gameObject, _a);

            }
            Destroy(col.gameObject);
            //ballsCollected.Peek().gameObject.transform.position.y - 1
            //myStack.Push(col.gameObject);
            //vut di-------
            //col.gameObject.transform.parent = this.transform;
            //Debug.Log(myStack.Peek());
        }
        else if (col.gameObject.tag == "Wall")
        {
            Debug.Log("Lose a ball");
            ballCount--;
            if (ballCount != 0)
            {  
                GameObject balls = myStack.Pop();
                balls.transform.parent = null;
                //wall logic
            }
            else
            {
                Debug.Log("Lose game");
                //GameObject.Find("LoseGame").SetActive(true);
                this.transform.Translate(Vector3.zero);
                going = false;
                lostGame = true;
                //lose game logic
            }
        }
    }

    IEnumerator calculationFinish(float a)
    {
        int xScore = 0;
        if (ballCount > 0)
        {
            for (int i = 1; i < myStack.Count; i = i + 0)
            {
                Destroy(myStack.Pop().gameObject);
                xScore += 5;
                score += xScore;
                Debug.Log(xScore);
                ballCount--;
                yield return new WaitForSeconds(a);
            }
        }
        winGame = true;


    }
    IEnumerator waitWin()
    {
        //win logic here
        yield return new WaitForSeconds(1f);

    }
    public void pushBall(GameObject ballP,Vector3 newPos)
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        GameObject myNewBall = Instantiate(ballP.gameObject, newPos, Quaternion.identity);
        myNewBall.transform.SetParent(transform);
        Destroy(ballP.gameObject);
        var center = this.GetComponent<BoxCollider>().center;
        this.GetComponent<BoxCollider>().center = new Vector3(center.x, center.y - 1, center.z);
        myStack.Push(myNewBall);
    }
}

//     public void CollectBall(Ball newBall)
//    {
//        ballsCollected.Push(newBall);
//        // Debug.LogWarning("ballsCollected.Count: " + ballsCollected.Count);

//        showTextPos = new Vector3(-0.7f, 1f, this.transform.position.z);
//        GameManager.instance.ShowFloatingText(($"+" + 1), 60, Color.yellow, showTextPos, Vector3.up * 60, 1f);

//        newBall.orderNumber = ballsCollected.Count + 1;

//        int characterState = ballsCollected.Count % 2 == 0 ? Constant.RUN_BACKWARD : Constant.RUN_FAST;
//        PostEventUpdateBall(characterState);

//        transform.position = new Vector3(transform.position.x, ballsCollected.Count - 1, transform.position.z);
//        // sphereCollider.center = new Vector3(sphereCollider.center.x, sphereCollider.center.y - 1, sphereCollider.center.z);

//        newBall.isCollected = true;

//        if (ballsCollected.Count < 1)
//        {
//            newBall.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
//        }
//        else
//        {
//            newBall.transform.position = new Vector3(transform.position.x, ballsCollected.Peek().gameObject.transform.position.y - 1, transform.position.z);
//        }
//        newBall.transform.SetParent(ballsContainer);

//        Vibrator.Vibrate(Constant.STRONG_VIBRATE);
//        SetSmokeFXPosition();

//    }
//}

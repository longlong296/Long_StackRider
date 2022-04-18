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
  
    public static bool going = false;
    public static int buttonNumber = 999;

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
        //making raycast is expensive
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            
           //Debug.Log(hit.collider.gameObject.name);
           limiter= hit.collider.gameObject.GetComponentInChildren<Renderer>().bounds.size.x;

        }

        //dy chuyen ve phia truoc
        if (!going == true) { 
        transform.Translate(new Vector3(0, 0, 4) * Time.deltaTime);
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
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

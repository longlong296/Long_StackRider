using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float fowardForce = 5000f;
    [SerializeField] float sidewayForce = 1500f;
    private Rigidbody rbChar;
    public static int score = 0;
    public float leftBound;
    public float rightBound;
    public static bool going = true;
    public static int buttonNumber = 999;


    // Start is called before the first frame update
    void Start()
    {
        rbChar = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()

    {

        RaycastHit hit;
        if (Physics.Raycast(Vector3.zero, Vector3.down, out hit))
        {
            Debug.DrawLine(hit.point, hit.normal);
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.GetComponent<Renderer>().bounds.size);

        }
        //rbChar.transform.forward*;
        //dy chuyen ve phia truoc
        if (!going.Equals(false))
        {
            rbChar.AddForce(0, 0, fowardForce * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
            {
                //Rotate the sprite about the Y axis in the positive direction
                rbChar.AddForce(sidewayForce * Time.deltaTime, 0, fowardForce * Time.deltaTime / 2);
                //set cho khoi cau dung im sau khi nhan nut xong
                //tranh tron truot
                if (Input.GetKeyUp(KeyCode.D)) { rbChar.AddForce(0, 0, fowardForce * Time.deltaTime / 2); }
            }




            if (Input.GetKey(KeyCode.A))
            {

                rbChar.AddForce(-sidewayForce * Time.deltaTime, 0, fowardForce * Time.deltaTime / 3);
                //set cho khoi cau dung im sau khi nhan nut xong
                //tranh tron truot
                if (Input.GetKeyUp(KeyCode.A)) { rbChar.AddForce(0, 0, fowardForce * Time.deltaTime / 3); }
            }

        }
        //should be raycasting to check the boundary
        invinsibleBoundary(3.3447f, -3.3447f);



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
}

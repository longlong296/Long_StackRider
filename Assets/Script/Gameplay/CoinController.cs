using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float angle=0f;
    public float rotationSpeed=60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler( 0.0f, angle, 0.0f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cubes : MonoBehaviour
{
   [SerializeField] private Rigidbody rb;
   

 

    // Update is called once per frame
    void Update()
    {
       Vector3 move = new Vector3(0, 0, -RoadManager.curSpeed);
        rb.MovePosition(transform.position + move * Time.deltaTime);
       // transform.Translate(0, 0,- RoadManager.curSpeed * Time.deltaTime);
        if(transform.localPosition.z < 15)
        {
            Destroy(this.gameObject);
        }
        
    }
}

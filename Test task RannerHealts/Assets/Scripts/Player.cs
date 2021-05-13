using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject _Player;
    private static Rigidbody rb;
   
    private float positionX = -6;
    private Vector3[] positions = new Vector3[5];
    private static int indRoad = 2;
  
    public RoadManager helper;

    private void Awake()
    {
        rb = _Player.GetComponent<Rigidbody>();
        for (int i = 0; i < 5; i++)
        {
            positions[i] = new Vector3(positionX, 1.50f, 24.6f);
            positionX += 3;
        }
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 cur = Vector3.Lerp(rb.transform.position, positions[indRoad], 0.2f);
        
        rb.MovePosition(cur);
        
    }

    public static void MoveLeft()
    {
        if (indRoad >0)
        {
            indRoad--;
        }
    }
    public static void MoveRight()
    {
        if (indRoad < 4)
        {
            indRoad++;
        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "cube")
        {
            helper.LossOfHealth();
            helper.ShowHealths();
            
        }
        if (collision.gameObject.tag == "boardLeft")
        {
            MoveRight();
            helper.LossOfHealth();
            helper.ShowHealths();
            

        }
        if (collision.gameObject.tag == "boardRight")
        {
            MoveLeft();
            helper.LossOfHealth();
            helper.ShowHealths();
            
        }
    }
   
}

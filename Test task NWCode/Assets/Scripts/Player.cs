using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject _Player;
    private static Rigidbody rb;
    public RoadManager helper;
    private float positionX = -3;
    private Vector3[] positions = new Vector3[3];
    private static int indRoad = 1;

    private void Awake()
    {
        rb = _Player.GetComponent<Rigidbody>();
        for (int i = 0; i < 3; i++)
        {
            positions[i] = new Vector3(positionX, 1.50f, 24.6f);
            positionX += 3;
        }
    }
    private void Update()
    {
        Vector3 cur = Vector3.Lerp(rb.transform.position, positions[indRoad], 0.2f);
        
        rb.MovePosition(cur);
    }

    public static void MoveLeft()
    {
        if (indRoad > 0)
        {
            indRoad--;
        }
    }
    public static void MoveRight()
    {
        if (indRoad < 2)
        {
            indRoad++;
        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "cube")
        {
            helper.RestartLevel();
            helper.GameOver.text = "GameOver";
        }
    }


}

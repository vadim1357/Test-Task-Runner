using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadManager : MonoBehaviour
{
    public Rigidbody roadPref; // префаб одного куска дороги
    private List<Rigidbody> allRoads = new List<Rigidbody>(); // список в котором будут храниться все куски дорог
    public  float speed = 25;
    public static float curSpeed = 0;
    public int roadCount = 5;
    public Text distance;
    private float dis;
    public GameObject cubePref;
    private List<GameObject> allCubes = new List<GameObject>();
    private Vector3[] positions = new Vector3[3];
    private int positionX = -3;
    private float check = 30;
    private float T = 1;
    public Text GameOver;
    public Button start;




    void Start()
    {
        for(int i =0; i <3; i++)
        {
            positions[i] = new Vector3(positionX, 1,110);
            positionX+=3;
        }
        
        RestartLevel();
       // StartLevel();
    }

    
    void Update()
    {

        if(curSpeed == 0)
        {
            return;
        }
        foreach(Rigidbody road in allRoads)
        {
            
            Vector3 move = new Vector3(0, 0, -RoadManager.curSpeed);
            Vector3 targetPosition = road.transform.position + move * Time.deltaTime;
            if (targetPosition.z < 15)
            {
                
                
                targetPosition.z += 15 * roadCount;
                

               
               

            }
            road.MovePosition(targetPosition);
            
        }
        
        if (dis - check == 0)
        {
            curSpeed += 2;
            check += 20;
           if(T>= 0.35)
            {
                T -= 0.05f;
            }
        }
        

    }
    public void RestartLevel()
    {
        start.gameObject.SetActive(true);
        StopAllCoroutines();
        T = 1;
        dis = 0;
        curSpeed = 0;
        while (allRoads.Count > 0)
        {
            Destroy(allRoads[0]);
            allRoads.RemoveAt(0);
        }
        while (allCubes.Count > 0)
        {
            Destroy(allCubes[0]);
            allCubes.RemoveAt(0);
        }
        for (int i = 0; i < roadCount; i++)
        {
            CreateRoad();
        }
        SwipeManager.instance.enabled = false;
    }
    private void CreateRoad()
    {
        Vector3 pos = Vector3.zero;
        if(allRoads.Count > 0)
        {
            pos = allRoads[allRoads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }
        Rigidbody next = Instantiate(roadPref,pos ,Quaternion.identity);
        next.transform.SetParent(transform);
        allRoads.Add(next);
    }
    public void StartLevel()
    {
        start.gameObject.SetActive(false);
        GameOver.text = "";
        curSpeed = speed;
        StartCoroutine(SpawnCubes());
        StartCoroutine(Distance());


        
        SwipeManager.instance.enabled = true;
        
    }
    IEnumerator  Distance()
    {
        while (true)
        {
            
            distance.text = dis.ToString();
            dis += 1;
            yield return new WaitForSeconds(T);
        }
        
        
       
    }
    IEnumerator SpawnCubes()
    {
        while (true)
        {
            GameObject newCube = Instantiate(cubePref, positions[Random.Range(0, positions.Length)], Quaternion.identity);
            GameObject newCube2 = Instantiate(cubePref, positions[Random.Range(0, positions.Length)], Quaternion.identity);
            allCubes.Add(newCube);
            allCubes.Add(newCube2);
            yield return new WaitForSeconds(T);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class Auction : MonoBehaviour
{
    private List<GameObject> carList = new List<GameObject>();
    private List<GameObject> carPosList = new List<GameObject>();
    public List<GameObject> carBoughtPosList = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        InitiateLists();

        CarPositioner();
    }

    // Update is called once per frame
    private void Update()
    {
        CarSelector();
    }

    private void InitiateLists()
    {
        foreach (GameObject carPos in GameObject.FindGameObjectsWithTag("CarPos"))
        {
            carPosList.Add(carPos);
        }
        
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Car"))
        {
            carList.Add(car);
        }
        
        foreach (GameObject carBoughtPos in GameObject.FindGameObjectsWithTag("CarBoughtPos"))
        {
            carBoughtPosList.Add(carBoughtPos);
        }
    }

    private void CarPositioner()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomCar = Random.Range(0, carList.Count);

            carList[randomCar].transform.position = carPosList[i].transform.position;

            carList.Remove(carList[randomCar]);
        }
    }

    private void CarSelector()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null) {
                    //Debug.Log("Hit " + hit.transform.gameObject.name);
                    
                    int randomCarBoughtPos = Random.Range(0, carBoughtPosList.Count);
        
                    hit.transform.position = carBoughtPosList[randomCarBoughtPos].transform.position;
                    carBoughtPosList.Remove(carBoughtPosList[randomCarBoughtPos]);
                }
        }
    }
}

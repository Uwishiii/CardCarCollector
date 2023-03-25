using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Auction : MonoBehaviour
{
    public List<GameObject> carList = new List<GameObject>();
    public List<GameObject> carPosList = new List<GameObject>();
    public List<GameObject> auctionCars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject carPos in GameObject.FindGameObjectsWithTag("CarPos"))
        {
            carPosList.Add(carPos);
        }
        
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Car"))
        {
            carList.Add(car);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomCar = Random.Range(0, carList.Count);

            //auctionCars.Add(carList[randomCar]);

            carList[randomCar].transform.position = carPosList[i].transform.position;

            carList.Remove(carList[randomCar]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

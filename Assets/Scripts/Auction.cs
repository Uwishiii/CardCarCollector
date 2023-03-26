using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Auction : MonoBehaviour
{
    private List<GameObject> carList = new List<GameObject>();
    private List<GameObject> carPosList = new List<GameObject>();
    private List<GameObject> carBoughtPosList = new List<GameObject>();
    [SerializeField] public GameObject gameCamera;
    [SerializeField] public GameObject gameCameraPosRight;
    private float timeLeft;
    
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
        
        //CameraMove(gameCameraPosRight.transform.position, gameCameraPosRight.transform.rotation, 3);
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
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Car"))
                {
                    int randomCarBoughtPos = Random.Range(0, carBoughtPosList.Count);
                    
                    hit.transform.position = carBoughtPosList[randomCarBoughtPos].transform.position;
                    hit.transform.rotation *= Quaternion.Euler(90, 0, 0);
                    carBoughtPosList.Remove(carBoughtPosList[randomCarBoughtPos]);
                }
        }
    }
    
    IEnumerator CameraMove2(int timeInSeconds)
    {
        while (timeLeft >= 0.0f)
        {
            gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, gameCameraPosRight.transform.position, Time.deltaTime * timeInSeconds);
            gameCamera.transform.rotation = Quaternion.Slerp(gameCamera.transform.rotation, gameCameraPosRight.transform.rotation, Time.deltaTime * timeInSeconds);

            Debug.Log(timeLeft);
            timeLeft -= Time.deltaTime;
            
            yield return null;
        }
    }

    public void CameraMove()
    {
        timeLeft = 3.0f;
        
        StartCoroutine(CameraMove2(3));
    }
}

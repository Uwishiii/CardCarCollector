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
    [SerializeField] public GameObject gameCameraPosLeft;
    private float timeLeft;
    [SerializeField] private CurrentPoints pointsScript;
    private int points;
    private int cost;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI middleText;
    public TextMeshProUGUI rightText;
    //change cost list back to private when done
    public List<int> costList = new List<int>();
    private List<TextMeshProUGUI> costTextslist = new List<TextMeshProUGUI>();


    // Start is called before the first frame update
    private void Start()
    {
        InitiateLists();
        CarPositioner();
        CostGenerator();
        CostShowcase();
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
        points = pointsScript.points;
        
        if (Input.GetMouseButtonDown(0) && points >= cost) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null && hit.transform.gameObject.CompareTag("Car"))
                {
                    int randomCarBoughtPos = Random.Range(0, carBoughtPosList.Count);
                    
                    hit.transform.position = carBoughtPosList[randomCarBoughtPos].transform.position;
                    hit.transform.rotation *= Quaternion.Euler(90, 0, 0);
                    carBoughtPosList.Remove(carBoughtPosList[randomCarBoughtPos]);
                    pointsScript.points -= 3;
                }
        }
    }

    private void CostGenerator()
    {
        for (int i = 0; i < 3; i++)
        {
            cost = Random.Range(0, 50);
            costList.Add(cost);
        }
        costTextslist.Add(leftText);
        costTextslist.Add(middleText);
        costTextslist.Add(rightText);
    }

    private void CostShowcase()
    {
        for (int i = 0; i < costList.Count; i++)
        {
            TextMeshProUGUI costTexts = costTextslist[i].GetComponent<TextMeshProUGUI>();
            costTexts.text = costList[i].ToString();
        }
    }
    
    IEnumerator CameraMove2(int timeInSeconds, Quaternion desiredRotation, Vector3 desiredPosition)
    {
        while (gameCamera.transform.position != desiredPosition && gameCamera.transform.rotation != desiredRotation)
        {
            gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, desiredPosition, Time.deltaTime * timeInSeconds);
            gameCamera.transform.rotation = Quaternion.Slerp(gameCamera.transform.rotation, desiredRotation, Time.deltaTime * timeInSeconds);

            //Debug.Log(timeLeft);
            timeLeft -= Time.deltaTime;
            
            yield return null;
        }
    }

    public void CameraMoveRight()
    {
        timeLeft = 3.0f;
        StopAllCoroutines();
        StartCoroutine(CameraMove2(3, gameCameraPosRight.transform.rotation, gameCameraPosRight.transform.position));
    }
    
    public void CameraMoveLeft()
    {
        timeLeft = 3.0f;
        StopAllCoroutines();
        StartCoroutine(CameraMove2(3, gameCameraPosLeft.transform.rotation, gameCameraPosLeft.transform.position));
    }
}

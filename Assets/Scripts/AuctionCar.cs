using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionCar : MonoBehaviour
{
    [SerializeField] private GameObject carBoughtPos;
    
    private void OnMouseDown()
    {
        transform.position = carBoughtPos.transform.position;
    }
}

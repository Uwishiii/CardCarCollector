using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = System.Random;

public class CurrentPoints : MonoBehaviour
{
    public int points = 0;
    public TextMeshProUGUI pointsGUI;
    
    private void Update()
    {
        pointsGUI.text = points.ToString();
    }
    
    public void addPoints()
    {
        points++;
    }
}

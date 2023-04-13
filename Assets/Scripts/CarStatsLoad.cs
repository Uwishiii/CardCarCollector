using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WanzyeeStudio.Json;

public class CarStatsLoad : MonoBehaviour
{
    public CarStats carStats = new CarStats();

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/CarStats.json";
        string carStatsData = System.IO.File.ReadAllText(filePath);
        carStats = JsonUtility.FromJson<CarStats>(carStatsData);
    }

    private void Start()
    {
        LoadFromJson();
        Debug.Log(carStats.cars[1].carModel);
    }
}

[Serializable]
public class CarStats
{
    public List<Cars> cars = new List<Cars>();
    public TextAsset jsonFile;
}

[Serializable]
public class Cars
{
    public string carModel;
    public float baseSpeed;
    public float baseAcceleration;
}
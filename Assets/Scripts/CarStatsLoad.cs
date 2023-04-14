using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using WanzyeeStudio.Json;

public class CarStatsLoad : MonoBehaviour
{
    public CarStats carStats = new CarStats();
    public TrackStats trackStats = new TrackStats();

    public void LoadCarsFromJson()
    {
        string filePath = Application.persistentDataPath + "/CarStats.json";
        string carStatsData = System.IO.File.ReadAllText(filePath);
        carStats = JsonUtility.FromJson<CarStats>(carStatsData);
    }
    
    public void LoadTracksFromJson()
    {
        string filePath = Application.persistentDataPath + "/TrackStats.json";
        string trackStatsData = System.IO.File.ReadAllText(filePath);
        trackStats = JsonUtility.FromJson<TrackStats>(trackStatsData);
    }

    private void Start()
    {
        LoadCarsFromJson();
        LoadTracksFromJson();
        Debug.Log(carStats.cars[1].carModel);
        Debug.Log(trackStats.tracks[1].straights);
    }

    public void RaceCalculation(int trackNr, int carNum1, int carNum2)
    {
        var car1 = carStats.cars[carNum1];
        var car2 = carStats.cars[carNum2];
        var track = trackStats.tracks[trackNr];

        float car1StraightResult = ((car1.baseSpeed + (car1.baseAcceleration * 0.5f)) * track.straights);
        float car1TurnResult = ((car1.baseAcceleration + car1.baseHandling + car1.tireQuality) * track.turns);
        float car1RoadResult = ((car1.baseHandling + car1.tireQuality) * track.road);
        float car1Result = car1StraightResult + car1TurnResult + car1RoadResult;
        
        float car2StraightResult = ((car2.baseSpeed + (car2.baseAcceleration * 0.5f)) * track.straights);
        float car2TurnResult = ((car2.baseAcceleration + car2.baseHandling + car2.tireQuality) * track.turns);
        float car2RoadResult = ((car2.baseHandling + car2.tireQuality) * track.road);
        float car2Result = car1StraightResult + car1TurnResult + car1RoadResult;

        if (car1Result > car2Result)
        {
            Debug.Log("Car 1 Wins!");
        }
        if (car1Result < car2Result)
        {
            Debug.Log("Car 2 Wins!");
        }
        if (Math.Abs(car1Result - car2Result) < 0.1)
        {
            Debug.Log("It's a Tie!");
        }
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
    public float baseHandling;
    public float tireQuality;
}

[Serializable]
public class TrackStats
{
    public List<Tracks> tracks = new List<Tracks>();
    public TextAsset jsonFile;
}

[Serializable]
public class Tracks
{
    public int trackNr;
    public float straights;
    public float turns;
    public float road;
}
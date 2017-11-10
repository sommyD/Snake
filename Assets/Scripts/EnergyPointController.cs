using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPointController : MonoBehaviour
{

    public GameObject energyPointPrefab;
    public GameObject currentEnergyPoint;
    private float planeWidth = 6;
    private Vector3 position;

    void AddEnergyPoint()
    {
        position = new Vector3((float)System.Math.Round(Random.Range(-planeWidth, planeWidth)), 0.5f, (float)System.Math.Round(Random.Range(-planeWidth, planeWidth)));
        currentEnergyPoint = GameObject.Instantiate(energyPointPrefab, position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentEnergyPoint)
            AddEnergyPoint();
        else
            return;

    }
}

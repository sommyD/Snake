using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusPointController : MonoBehaviour
{

    public GameObject minusPointPrefab;
    public GameObject currentMinusPoint;
    private float planeWidth = 6;
    private Vector3 position;

    void AddMinusPoint()
    {
        position = new Vector3((float)System.Math.Round(Random.Range(-planeWidth, planeWidth)), 0.5f, (float)System.Math.Round(Random.Range(-planeWidth, planeWidth)));
        currentMinusPoint = GameObject.Instantiate(minusPointPrefab, position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentMinusPoint)
            AddMinusPoint();
        else
            return;

    }
}

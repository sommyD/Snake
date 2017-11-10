using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePointController : MonoBehaviour
{

    public GameObject reversePointPrefab;
    public GameObject currentReversePoint;
    private float planeWidth = 6;
    private Vector3 position;

    void AddReversePoint()
    {
        position = new Vector3((float)System.Math.Round(Random.Range(-planeWidth, planeWidth)), 0.5f, (float)System.Math.Round(Random.Range(-planeWidth, planeWidth)));
        currentReversePoint = GameObject.Instantiate(reversePointPrefab, position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentReversePoint)
            AddReversePoint();
        else
            return;

    }
}

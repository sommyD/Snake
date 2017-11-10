using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusPointController : MonoBehaviour
{

    public GameObject plusPointPrefab;
    public GameObject currentPlusPoint;
    private float planeWidth = 6;
    private Vector3 position;

    void AddPlusPoint()
    {
        position = new Vector3((float)System.Math.Round(Random.Range(-planeWidth, planeWidth)), 0.5f, (float)System.Math.Round(Random.Range(-planeWidth, planeWidth)));
        currentPlusPoint = GameObject.Instantiate(plusPointPrefab, position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentPlusPoint)
            AddPlusPoint();
        else
            return;

    }
}

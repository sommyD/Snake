using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeHeadMovement : MonoBehaviour {
    public float speed;
    public float planeWidth=6.5f;
    public List<GameObject> tailObjects = new List<GameObject>();
    public GameObject SnakeTailPrefab;
    public SnakeTailMovement snakeTail;
    public float offsetZ = -1f;
    public bool isPrepearingToTurn = false;
    public bool isInLastTurnZone = false;
    public float rotationValue;
    float keyDownX;
    float keyDownZ;
    public int direction;
    public bool isReverse=false;
    public bool isEnergy = false;
    public float energyTime = 2;
    float energyTimeStart;
    private float normalSpeed = 6;
    private float energySpeed = 10;

    // Use this for initialization
    void Start () {

        tailObjects.Add(gameObject);
        setDirection();
        speed = normalSpeed;
        snakeTail = SnakeTailPrefab.GetComponent<SnakeTailMovement>();
    }
	
	// Update is called once per frame
	void Update () {

        if (isEnergy && Time.time - energyTimeStart > energyTime)
        {
            speed = normalSpeed;
            isEnergy = false;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (isInLastTurnZone)
            CheckIsInLastTurnZone();


        if (Input.GetKeyDown(KeyCode.D))
        {
            isPrepearingToTurn = true;
            rotationValue = (float)(System.Math.PI / 2);
            keyDownX = transform.position.x;
            keyDownZ = transform.position.z;
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            isPrepearingToTurn = true;
            rotationValue = -(float)(System.Math.PI/2);
            keyDownX = transform.position.x;
            keyDownZ =transform.position.z;
        }


        if (isPrepearingToTurn)
            CheckIsTurning();
        CheckIsOutOfPlane();
     
    }

    public void CheckIsTurning()
    {
        if      (direction == 0 && !isInLastTurnZone &&
                (transform.position.z - System.Math.Floor(keyDownZ) < 0.15f || transform.position.z - System.Math.Floor(keyDownZ) > 1f))
            Turn(System.Math.Sign(rotationValue) * (transform.position.z - System.Math.Floor(transform.position.z)), 0);

        else if (direction == 1 && !isInLastTurnZone &&
                (transform.position.x - System.Math.Floor(keyDownX) < 0.15f || transform.position.x - System.Math.Floor(keyDownX) > 1f))
            Turn(0, -System.Math.Sign(rotationValue) * (transform.position.x - System.Math.Floor(transform.position.x)));
   
        else if (direction == 2 && !isInLastTurnZone &&
                (System.Math.Ceiling(keyDownZ) - transform.position.z < 0.15f || System.Math.Ceiling(keyDownZ) - transform.position.z > 1))
            Turn(-System.Math.Sign(rotationValue) * (System.Math.Ceiling(transform.position.z) - transform.position.z), 0);

        else if (direction == 3 && !isInLastTurnZone &&
               (System.Math.Ceiling(keyDownX) - transform.position.x < 0.15f || System.Math.Ceiling(keyDownX) - transform.position.x > 1))
            Turn(0, System.Math.Sign(rotationValue) * (System.Math.Ceiling(transform.position.x) - transform.position.x));
        }

    public void Turn(double offsetX,double offsetZ)
    {
        transform.RotateAroundLocal(Vector3.up, rotationValue);
        Vector3 position = transform.position;

        position.x = (float)(System.Math.Round(position.x) + offsetX);
        position.z = (float)(System.Math.Round(position.z) + offsetZ);
        transform.SetPositionAndRotation(position, transform.rotation);
        setDirection();

        isPrepearingToTurn = false;
        isInLastTurnZone = true;
    }
    public void setDirection()
    {
        if (System.Math.Round(transform.rotation.eulerAngles.y) == 0)
            direction = 0;
        else if (System.Math.Round(transform.rotation.eulerAngles.y) == 90)
            direction = 1;
        else if (System.Math.Round(transform.rotation.eulerAngles.y) == 180)
            direction = 2;
        else
            direction = 3;
    }
    public void CheckIsInLastTurnZone()
    {
      isInLastTurnZone=System.Math.Abs(transform.position.x - System.Math.Round(transform.position.x)) < 0.15f && 
                       System.Math.Abs(transform.position.z - System.Math.Round(transform.position.z)) < 0.15f;
    }
    public void AddTail()
    {
        Transform lastPieceTransform= tailObjects[tailObjects.Count -1].transform;
        float angleY =(float)(lastPieceTransform.rotation.eulerAngles.y*System.Math.PI/180f);

        float x = lastPieceTransform.position.x + (float)(-System.Math.Sin(-angleY) * offsetZ);
        float z = lastPieceTransform.position.z + (float)( System.Math.Cos(-angleY) * offsetZ);

        Vector3 tailPosition = new Vector3(x,lastPieceTransform.position.y,z);

        tailObjects.Add (GameObject.Instantiate(SnakeTailPrefab, tailPosition, Quaternion.identity) as GameObject);
        tailObjects[tailObjects.Count - 1].transform.SetPositionAndRotation(tailPosition, lastPieceTransform.rotation);
    }

    public void RemoveTail()
    {
        Destroy(tailObjects[tailObjects.Count - 1]);
        tailObjects.RemoveAt(tailObjects.Count-1);
        if (tailObjects.Count==0)
            Application.LoadLevel(Application.loadedLevel);
    }
    public void ChangeSpeed()
    {
        speed = energySpeed;
        energyTimeStart = Time.time;
        isEnergy = true;
    }
    public void Reverse()
    {
        if (tailObjects.Count == 1)
        {
            transform.RotateAroundLocal(Vector3.up, (float)System.Math.PI);
            isPrepearingToTurn = false;
            isInLastTurnZone = false;
        }
        else isReverse = true;
    }

    public void CheckIsOutOfPlane()
    {
        if (System.Math.Abs(transform.position.x) > planeWidth)
        {
            Vector3 position = transform.position;
            position.x = (2 * planeWidth - System.Math.Abs(position.x)) * -System.Math.Sign(position.x);
            keyDownX = (2 * planeWidth - System.Math.Abs(keyDownX)) * -System.Math.Sign(keyDownX);
            transform.SetPositionAndRotation(position, transform.rotation);
        }
        if (System.Math.Abs(transform.position.z) > planeWidth)
        {
            Vector3 position = transform.position;
            position.z = (2 * planeWidth - System.Math.Abs(position.z)) * -System.Math.Sign(position.z);
            keyDownZ = (2 * planeWidth - System.Math.Abs(keyDownZ)) * -System.Math.Sign(keyDownZ);
            transform.SetPositionAndRotation(position, transform.rotation);
        }
    }

}

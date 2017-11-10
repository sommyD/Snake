using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeTailMovement : MonoBehaviour {

    public GameObject previousPiece;
    public SnakeHeadMovement snakeHead;
    List<GameObject> tailObjects;
    public int index;
    public bool isPrepearingToTurn;
    public float rotationValue;
    public float turnX;
    public float turnZ;
    float lastX;
    float lastZ;
    public int direction;
    // Use this for initialization
    void Start() {
        snakeHead = GameObject.FindGameObjectWithTag("SnakeHead").GetComponent<SnakeHeadMovement>();
        tailObjects = snakeHead.tailObjects;
        previousPiece = tailObjects[snakeHead.tailObjects.Count - 2];
        index = snakeHead.tailObjects.IndexOf(gameObject);
        SetDirection();
        //print(index);
    }

    // Update is called once per frame
    void Update() {
        lastX = transform.position.x;
        lastZ = transform.position.z;
        transform.Translate(Vector3.forward * Time.deltaTime * snakeHead.speed);

        SetDirection();
        if (!isPrepearingToTurn)
            CheckIsPrepearingToTurn();
        if (isPrepearingToTurn)
            CheckIsTurning();
        CheckIsOutOfPlane();
        if (index == tailObjects.Count - 1 && snakeHead.isReverse)
            Reverse();
    }

    public void SetDirection()
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

    private void CheckIsPrepearingToTurn()
    {
        if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) != System.Math.Round(transform.rotation.eulerAngles.y))
            isPrepearingToTurn = true;
        else return;


        if (System.Math.Round(transform.rotation.eulerAngles.y) == 0)
        {
            if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y)==270)
                rotationValue = -(float)(System.Math.PI / 2);
            else if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 90)
                rotationValue = (float)(System.Math.PI / 2);
        }

        if (System.Math.Round(transform.rotation.eulerAngles.y) == 90)
        {
            if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 0)
                rotationValue = -(float)(System.Math.PI / 2);
            else if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 180)
                rotationValue = (float)(System.Math.PI / 2);
        }

        if (System.Math.Round(transform.rotation.eulerAngles.y) == 180)
        {
            if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 90)
                rotationValue = -(float)(System.Math.PI / 2);
            else if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 270)
                rotationValue = (float)(System.Math.PI / 2);
        }

        if (System.Math.Round(transform.rotation.eulerAngles.y) == 270)
        {
            if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 180)
                rotationValue = -(float)(System.Math.PI / 2);
            else if (System.Math.Round(previousPiece.transform.rotation.eulerAngles.y) == 0)
                rotationValue = (float)(System.Math.PI / 2);
        }
      
        if (direction == 1 || direction==3)
            turnX = (float)System.Math.Round(previousPiece.transform.position.x);
        else
            turnZ = (float)System.Math.Round(previousPiece.transform.position.z);
    }

    private void CheckIsTurning()
    {
        if (direction == 0 && transform.position.z >= turnZ && lastZ < turnZ)
            Turn(System.Math.Sign(rotationValue) * (transform.position.z - System.Math.Floor(transform.position.z)), 0);

        else if (direction == 1 && transform.position.x >= turnX && lastX < turnX)
            Turn(0, -System.Math.Sign(rotationValue) * (transform.position.x - System.Math.Floor(transform.position.x)));

        else if (direction == 2 && transform.position.z <= turnZ && lastZ > turnZ)
            Turn(-System.Math.Sign(rotationValue) * (System.Math.Ceiling(transform.position.z) - transform.position.z), 0);

        else if (direction == 3 && transform.position.x <= turnX && lastX > turnX)
            Turn(0, System.Math.Sign(rotationValue) * (System.Math.Ceiling(transform.position.x) - transform.position.x));
    }

    private void Turn(double offsetX, double offsetZ)
    {
        transform.RotateAroundLocal(Vector3.up, rotationValue);
        Vector3 position = transform.position;
        position.x = (float) (System.Math.Round(position.x) + offsetX);
        position.z = (float) (System.Math.Round(position.z) + offsetZ);
        transform.SetPositionAndRotation(position, transform.rotation);
        SetDirection();
        isPrepearingToTurn = false;
    }


    private void CheckIsOutOfPlane()
    {
        if (System.Math.Abs(transform.position.x) > snakeHead.planeWidth)
        {
            Vector3 position = transform.position;
            position.x = (2 * snakeHead.planeWidth - System.Math.Abs(position.x)) * -System.Math.Sign(position.x);
            transform.SetPositionAndRotation(position, transform.rotation);
        }
        if (System.Math.Abs(transform.position.z) > snakeHead.planeWidth)
        {
            Vector3 position = transform.position;
            position.z = (2 * snakeHead.planeWidth - System.Math.Abs(position.z)) * -System.Math.Sign(position.z);
            transform.SetPositionAndRotation(position, transform.rotation);
        }
    }

    public void Reverse()
    {
        for (int i = 0; i < tailObjects.Count / 2; i++)
        {
            Vector3 positionBuf = tailObjects[tailObjects.Count - i - 1].transform.position;
            Quaternion rotationBuf = tailObjects[tailObjects.Count - i - 1].transform.rotation;
            tailObjects[tailObjects.Count - i - 1].transform.SetPositionAndRotation(tailObjects[i].transform.position, tailObjects[i].transform.rotation);
            tailObjects[i].transform.SetPositionAndRotation(positionBuf, rotationBuf);
        }

        for (int i = 0; i < tailObjects.Count; i++)
        {
            tailObjects[i].transform.RotateAroundLocal(Vector3.up, (float)System.Math.PI);
            if (i == 0)
            {
                snakeHead.isPrepearingToTurn = false;
                snakeHead.isInLastTurnZone = false;
                snakeHead.setDirection();
            }
            else
            {
                tailObjects[i].GetComponent<SnakeTailMovement>().isPrepearingToTurn = false;
                tailObjects[i].GetComponent<SnakeTailMovement>().SetDirection();
            }
        }
        snakeHead.isReverse = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeHead") && index>1)
            Application.LoadLevel(Application.loadedLevel);

    }
}

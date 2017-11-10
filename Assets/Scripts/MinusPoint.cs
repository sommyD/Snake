using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeHead"))
        {
            other.GetComponent<SnakeHeadMovement>().RemoveTail();
            Destroy(gameObject);
        }
    }
}

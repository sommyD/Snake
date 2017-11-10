using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeHead"))
        {
            other.GetComponent<SnakeHeadMovement>().Reverse();
            Destroy(gameObject);
        }
    }
}

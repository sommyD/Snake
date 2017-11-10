using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeHead"))
        {
            other.GetComponent<SnakeHeadMovement>().AddTail();
            Destroy(gameObject);
        }
    }
}

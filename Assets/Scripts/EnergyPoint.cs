using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeHead"))
        {
            other.GetComponent<SnakeHeadMovement>().ChangeSpeed();
            Destroy(gameObject);
        }
    }
}

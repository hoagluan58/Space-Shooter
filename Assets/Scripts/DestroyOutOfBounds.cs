using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 0f;
    private float lowerBound = -3.0f;

    void Update()
    {
        if (transform.position.y > topBound)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        if (transform.position.y < lowerBound)
        {

            Destroy(gameObject);
        }
    }
}

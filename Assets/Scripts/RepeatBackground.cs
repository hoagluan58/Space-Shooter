using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startpos;
    private float repeatHeight;
    private float speed = 1.0f;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        startpos = transform.position;
        repeatHeight = GetComponent<BoxCollider>().size.y / 2;
    }

    void Update()
    {
        if ((transform.position.y < startpos.y - repeatHeight) && playerControllerScript.gameOver == false)
        {
            transform.position = startpos;
        }
        BackgroundMoveDown();
    }

    void BackgroundMoveDown()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}

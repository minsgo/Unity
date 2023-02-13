using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool playerCheck;
    PlatformEffector2D platformOject;

    // Start is called before the first frame update
    void Start()
    {
        playerCheck = false;
        platformOject = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && playerCheck)
        {
            platformOject.rotationalOffset = 180f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            platformOject.rotationalOffset = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCheck = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCheck = false;
    }
}



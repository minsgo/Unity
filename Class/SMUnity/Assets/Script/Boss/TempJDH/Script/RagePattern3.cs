using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagePattern3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("µ¥¹ÌÁö");
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelightPattern2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
            Debug.Log("Delight 패턴 2 데미지");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

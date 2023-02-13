using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadPattern1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, 2.0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
            Debug.Log("새드 패턴 1 데미지");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

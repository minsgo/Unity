using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadPattern2Attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("새드 패턴 2 데미지");
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelightPattern3_att : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //player damage
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;

            //boss heal
            DelightBossDirector Boss_Heal = GameObject.FindGameObjectWithTag("Object").GetComponent<DelightBossDirector>();
            Boss_Heal.Heal(10);
            
            Debug.Log("Delight 패턴 3 데미지");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

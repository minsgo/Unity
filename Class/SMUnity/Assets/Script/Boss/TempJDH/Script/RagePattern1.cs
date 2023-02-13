using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagePattern1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("���� 1 ������");
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
            OnDamaged();
            SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            sound.SoundPlay("DAMAGED");

        }
    }
    void OnDamaged() //�ǰ� ���� �� ����
    {
        SpriteRenderer sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        gameObject.layer = 9;
        sprite.color = new Color32(255, 255, 255, 55);
        Invoke("OffDamaged", 3);
    }

    void OffDamaged() //�ǰ� �� ���� ���� ����
    {
        SpriteRenderer sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        gameObject.layer = 8;
        sprite.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

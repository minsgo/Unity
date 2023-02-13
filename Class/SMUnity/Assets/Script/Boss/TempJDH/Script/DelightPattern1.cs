using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelightPattern1 : MonoBehaviour
{
    UnityEngine.Vector3 speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
            Debug.Log("Delight 패턴 1 데미지");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
        speed = new Vector3(0.0f,0.0f,0.0f);
    }
    public void SetSpeed(UnityEngine.Vector3 sv)
    {
        speed = sv;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RagePattern2 : MonoBehaviour
{
    UnityEngine.Vector3 speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("패턴 2 데미지");
            PlayerController HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            HP.HP -= 30;
        }
    }

    public void SetSpeed(UnityEngine.Vector3 sv)
    {
        speed = sv;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
        speed = new UnityEngine.Vector3(0.0f,0.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime);
    }
}

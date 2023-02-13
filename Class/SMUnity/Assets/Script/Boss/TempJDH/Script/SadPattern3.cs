using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadPattern3 : MonoBehaviour
{
    float Speed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,4.0f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("패턴 3 충돌");
            Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
            rigid.velocity = new Vector2(-6.0f, rigid.velocity.y);
        }
    }

    public void SetSpeed(float Sp)
    {
        Speed = Sp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Speed * Time.deltaTime,0.0f,0.0f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemySpawn enemySpawn;
    Rigidbody2D rigid;
    SpriteRenderer sp;
    bool isdie = false;
    public int ActionNum;

    // Start is called before the first frame update
    public void Setup(EnemySpawn enemySpawn){
        this.enemySpawn = enemySpawn;
    }
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
    }
    void FixedUpdate()
    {
        Thinking();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Think()
    {
        float time = Random.Range(2f, 5f);
        ActionNum = Random.Range(-1, 2);
        Invoke("Think", time);
    }
    void Thinking()
    {
        rigid.velocity = new Vector2(ActionNum, rigid.velocity.y);
        Vector2 front = new Vector2(rigid.position.x + ActionNum, rigid.velocity.y); //��ĭ�� �˾ƾ���
        Debug.DrawRay(front, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D raycast = Physics2D.Raycast(front, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (raycast.collider == null)
        {
            ActionNum = ActionNum * (-1);
            CancelInvoke();
            Invoke("Think", 5);
        }
    }
    public void OnDie(){
        enemySpawn.DestroyEnemy(this);
    }
}

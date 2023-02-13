using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : MonoBehaviour
{
    private float moveSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);
    }
    //�¾��� ��� 5�ʰ� ���� �� �ı�
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Delight"))
        {
            //��ų �����ؾߵ�. �̿ϼ�
            DelightBossDirector del_Damaged = GameObject.Find("DelightBossDirector").GetComponent<DelightBossDirector>();
            del_Damaged.Hit(7);
            Destroy(this.gameObject);
        }
        else if (other.tag.Equals("Rage"))
        {
            RageBossDirector rage_Damaged = GameObject.Find("RageBossDirector").GetComponent<RageBossDirector>();
            rage_Damaged.Hit(7);
            Destroy(this.gameObject);
        }
        else if (other.tag.Equals("Sad"))
        {
            SadBossDirector sad_Damaged = GameObject.Find("SadBossDirector").GetComponent<SadBossDirector>();
            sad_Damaged.Hit(7);
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float f)
    {
        moveSpeed = 3f * f;
    }
}
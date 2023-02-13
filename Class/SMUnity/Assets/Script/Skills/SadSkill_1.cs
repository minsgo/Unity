using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadSkill_1 : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Delight"))
        {
            //스킬 구현해야됨. 미완성
            DelightBossDirector del_Damaged = GameObject.Find("DelightBossDirector").GetComponent<DelightBossDirector>();
            del_Damaged.Hit(5);
            Destroy(this.gameObject);
        }
        else if (other.tag.Equals("Rage"))
        {
            RageBossDirector rage_Damaged = GameObject.Find("RageBossDirector").GetComponent<RageBossDirector>();
            rage_Damaged.Hit(5);
            Destroy(this.gameObject);
        }
        else if (other.tag.Equals("Sad"))
        {
            SadBossDirector sad_Damaged = GameObject.Find("SadBossDirector").GetComponent<SadBossDirector>();
            sad_Damaged.Hit(5);
            Destroy(this.gameObject);
        }   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageWeakEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Delight")
        {
            //스킬 구현해야됨. 미완성
            DelightBossDirector del_Damaged = GameObject.Find("DelightBossDirector").GetComponent<DelightBossDirector>();
            del_Damaged.Hit(5);
        }
        else if(other.gameObject.tag == "Rage")
        {
            RageBossDirector rage_Damaged = GameObject.Find("RageBossDirector").GetComponent<RageBossDirector>();
            rage_Damaged.Hit(5);
        }
        else if(other.gameObject.tag == "Sad")
        {
            SadBossDirector sad_Damaged = GameObject.Find("SadBossDirector").GetComponent<SadBossDirector>();
            sad_Damaged.Hit(5);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

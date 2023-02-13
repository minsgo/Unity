using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBossHit : BossHit
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindWithTag("Director");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDamaged(int n)
    {
        director.GetComponent<RageBossDirector>().Hit(n);
    }
}

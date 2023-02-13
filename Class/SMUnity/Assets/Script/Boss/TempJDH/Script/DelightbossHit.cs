using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelightbossHit : BossHit
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
        director.GetComponent<DelightBossDirector>().Hit(n);
    }
}

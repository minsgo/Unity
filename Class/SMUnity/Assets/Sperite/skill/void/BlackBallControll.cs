using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBallControll : MonoBehaviour
{
    public GameObject blackBallP;
    public Transform blackBallP_Pos;
    float fireTimer=0;
    const float fireDelay=0.5f;
    
    public GameObject blackBallH;
    public Transform blackBallH_Pos;
    float fireTimer2=0;
    const float fireDelay2=1f;
    
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        FireControll();
        Fire2Controll();
    }

    void FireControll()
    {
        if(fireTimer>fireDelay&&Input.GetKey(KeyCode.C))
        {
            GameObject obj = Instantiate(blackBallP, blackBallP_Pos.position, Quaternion.identity);
            obj.GetComponent<Fire>().SetSpeed(1);
            fireTimer=0;
        }
        fireTimer+=Time.deltaTime;
    }

    void Fire2Controll()
    {
        if(fireTimer2>fireDelay2&&Input.GetKey(KeyCode.V))
        {
            Instantiate(blackBallH, blackBallH_Pos.position, Quaternion.identity);
            fireTimer2=0;
        }
        fireTimer2+=Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadPattern2 : MonoBehaviour
{
    public Sprite Normalstatus;
    public Sprite AttackStatus;
    public GameObject Lightning;
    private GameObject Player;
    private SpriteRenderer Spre;

    enum CloudState {wait, chase, warning};
    private CloudState cloudstate = CloudState.wait;
    Vector3 pos;
    float lightposxstart;
    float lightposxstride;
    float lightposy;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Spre = GetComponent<SpriteRenderer>();

        lightposxstart = -(5.76f / 2) + 0.9f;//-Spre.bounds.size.x/2 + Lightning.GetComponent<SpriteRenderer>().bounds.size.x;
        lightposxstride = 5.76f / 3.0f;//Lightning.GetComponent<SpriteRenderer>().bounds.size.x * 2;
        lightposy = -2.0f;//-Lightning.GetComponent<SpriteRenderer>().bounds.size.y / 2 - Spre.bounds.size.y / 2;

        Destroy(gameObject, 9.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CloudState.chase == cloudstate)
        {
            pos = Player.transform.position;
            pos.y += GetComponent<SpriteRenderer>().bounds.size.y * 3;
            this.transform.position = pos;
        }
        else if(cloudstate == CloudState.wait)
        {
            StartCoroutine(CloudChaseUpdate());
        }

    }
    IEnumerator CloudChaseUpdate()
    {
        cloudstate = CloudState.chase;
        yield return new WaitForSeconds(2.0f);

        Spre.sprite = AttackStatus;
        cloudstate = CloudState.warning;
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 3; i++) 
            Instantiate(Lightning, new Vector3(this.transform.position.x+ lightposxstart + lightposxstride * i, this.transform.position.y + lightposy, 0.0f), Quaternion.identity);
        
        yield return null;
        yield return new WaitForSeconds(0.2f);
        Spre.sprite = Normalstatus;
        cloudstate = CloudState.wait;
    }
}

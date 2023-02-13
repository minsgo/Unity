using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelightPattern3_warn : MonoBehaviour
{
    public GameObject attackprefab;
    private bool des = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InsAfterDes());
    }

    // Update is called once per frame
    void Update()
    {
        if(des)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator InsAfterDes()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(attackprefab,transform.position, Quaternion.identity);
        des = true;
       
    }
}

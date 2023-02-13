using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SadPattern1Warn : MonoBehaviour
{
    bool alpha = false;
    SpriteRenderer spr;
    float alphavar = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.0f);
        spr= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha)
        {
            alphavar += Time.deltaTime;
            float tempalpha = alphavar > 0.5f ? (1.0f - alphavar) * 2 : alphavar * 2;
            spr.color = new Color(1.0f,1.0f,1.0f, tempalpha);
        }
    }

    public void SetAlpha(bool v)
    {
        alpha = v;
    }
}

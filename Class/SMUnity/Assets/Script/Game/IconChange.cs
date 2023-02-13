using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{

    public Sprite Rage_icon;
    public Sprite Sad_icon;
    public Sprite Void_icon;
    public Sprite Delight_icon;

    Image nowimg;


    // Start is called before the first frame update
    void Start()
    {
        nowimg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Changeimg();
    }
    void Changeimg()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            nowimg.sprite = Void_icon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowimg.sprite = Sad_icon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowimg.sprite = Delight_icon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nowimg.sprite = Rage_icon;
        }
    }
    
}

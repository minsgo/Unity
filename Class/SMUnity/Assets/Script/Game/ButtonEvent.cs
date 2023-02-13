using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public Button Rage;
       
    // Start is called before the first frame update
    void Start()
    {
        Rage = GetComponent<Button>();
    }
    private void OnMouseEnter()
    {
        Rage.SendMessage("ButtonEventMessage");
    }
    void ButtonEventMessage()
    {
        Debug.Log("!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

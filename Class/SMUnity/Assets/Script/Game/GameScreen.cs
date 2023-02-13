using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetResolution()
    {
        int setWidth = 950;
        int setHeight = 555;

        Screen.SetResolution(setWidth, setHeight, true);
    }
}

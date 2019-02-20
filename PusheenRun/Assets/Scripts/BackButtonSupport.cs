using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonSupport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donme : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
       if(gameObject.tag == "Altin")
        {
            transform.Rotate(0, 0, 1*Time.deltaTime * 400f);
        }
        if (gameObject.tag == "Miknatis")
        {
            transform.Rotate(1*Time.deltaTime *400f, 0, 0);
        }
    }
}

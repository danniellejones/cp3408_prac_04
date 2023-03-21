using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour
{
    float speed = 1.0f;
    float halfSpeed = 1.0f;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, Time.deltaTime * halfSpeed, Time.deltaTime * speed);
  
    }
}

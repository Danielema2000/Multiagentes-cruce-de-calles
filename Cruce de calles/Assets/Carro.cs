using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Carro : MonoBehaviour
{
    public Rigidbody rb;
    float Speed = 7.5f;
    Vector3 cruce = new Vector3(1f, 0.5f, -4f);
    float t1, t2, t3;
    float t= 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t1 = (22.5f-4f)/Speed;
        t2 = t1+0.5f;
        t3 = t2+t1;
    }

    // Update is called once per frame
    void Update()
    {
        float cos = (float)Math.Cos(Math.PI);
        if(t<t1){
            rb.velocity = new Vector3(0, 0, Speed);
        }
        else{
            rb.velocity = new Vector3(0, 0, 0);
        }
        t+=Time.deltaTime;
    }
}

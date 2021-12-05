using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class Lectura : MonoBehaviour
{
    int numCars;
    public GameObject[] modelos;
    public List<GameObject> carros;
    public Light[] luces;
    public int ti = 0;
    int maxT;
    int lastGreen;
    //Carros
    List<int> id = new List<int>();
    List<int> t = new List<int>();
    List<float> x = new List<float>();
    List<float> y = new List<float>();
    //Semaforos
    List<int> id2 = new List<int>();
    List<int> t2 = new List<int>();
    List<bool> on = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        var reader = new StreamReader(File.OpenRead(@"C:\Users\danie\Documents\GitHub\Multiagentes-cruce-de-calles\carros.csv"));
        
        reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
            id.Add(Int32.Parse(values[0]));
            t.Add(Int32.Parse(values[1]));
            x.Add(float.Parse(values[2]));
            y.Add(float.Parse(values[3]));
        }
        numCars = id[id.Count - 1];
        maxT = t[t.Count - 1];
        for (int i = 0; i < numCars; i++)
        {
            carros.Add(Instantiate<GameObject>(modelos[i%4]));
        }
        for (int i = 0; i < numCars; i++)
        {
            int ii = i * (maxT + 1);
            float myX = x[ii], myY = y[ii];
            if (myX>65f)
            {
                carros[i].transform.Rotate(0f, 270f, 0f);
            }
            else if (myX<30)
            {
                carros[i].transform.Rotate(0f, 90f, 0f);
            }
            else if (myY<25)
            {
                carros[i].transform.Rotate(0f, 0f, 0f);
            }
            else
            {
                carros[i].transform.Rotate(0f, 180f, 0f);
            }
        }

        var reader2 = new StreamReader(File.OpenRead(@"C:\Users\danie\Documents\GitHub\Multiagentes-cruce-de-calles\semaforos.csv"));

        reader2.ReadLine();
        while (!reader2.EndOfStream)
        {
            var line = reader2.ReadLine();
            var values = line.Split(',');
            id2.Add(Int32.Parse(values[0]));
            t2.Add(Int32.Parse(values[1]));
            on.Add(bool.Parse(values[2]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ti <= maxT)
        {
            for (int i = 0; i < numCars; i++)
            {
                int ii = ti + i * (maxT + 1);
                float myX = x[ii], myY = y[ii];
                carros[i].transform.position = new Vector3(myX, 3f, myY);
            }
            for (int i = 0; i < 4; i++)
            {
                int ii = ti + i * (maxT + 1);
                if (on[ii])
                {
                    luces[i].color = Color.green;
                    lastGreen = i;
                }
                else
                {
                    if (lastGreen == i)
                    {
                        luces[i].color = Color.yellow;
                    }
                    else
                    {
                        luces[i].color = Color.red;
                    }
                }
            }
        }
        ti++;
        Thread.Sleep(100);
    }
}

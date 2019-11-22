using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float speed = 100;
    public GameObject Tatle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = transform.up;
        if (Tatle.transform.position.y < 204.1)
        {
            Tatle.transform.position += moveDirection * speed;
            return;
        }
    }
}   

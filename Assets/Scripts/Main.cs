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
        //bool a = false;
        //while (a == false)
        //{
        //    Tatle.transform.localPosition = new Vector3()
        //}
        Vector3 moveDirection = transform.up;
        Tatle.transform.position += moveDirection * speed;
        Debug.Log(Tatle.transform.position);
        if (Tatle.transform.position.y == 204.1)
        {
            Tatle.transform.position = Tatle.transform.position;
        }
    }
}   

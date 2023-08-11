using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoNube : MonoBehaviour
{

    public float velocidad;
    
    void Update()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        velocidad *= Time.deltaTime;
        
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, velocidad, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(velocidad, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -velocidad, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-velocidad, 0, 0);
        }
    }
}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Controlar : MonoBehaviour
{

    private Vector2 ejes;
    public float velocidad_de_movimiento;

    void Update()
    {
        ejes = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      
        ejes *= velocidad_de_movimiento * Time.deltaTime;
       
        transform.Translate(ejes.x, 0f, ejes.y, Space.World);
        
    }
}

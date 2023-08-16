using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Saltar : MonoBehaviour
{
    public float fuerza_de_salto;
    private Rigidbody fisicas;

    private void Start()
    {
        fisicas = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fisicas.AddForce(Vector3.up * fuerza_de_salto);
        }
    }
}

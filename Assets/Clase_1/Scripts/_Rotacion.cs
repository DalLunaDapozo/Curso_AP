using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Rotacion : MonoBehaviour
{

    public float velocidad_de_rotacion;

    void Update()
    {
        transform.Rotate(0f, velocidad_de_rotacion * Time.deltaTime, 0f);
    }
}

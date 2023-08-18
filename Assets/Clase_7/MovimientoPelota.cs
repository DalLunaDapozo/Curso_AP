using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPelota : MonoBehaviour
{
    public float velocidad_de_movimiento;
    public float fuerza_salto;

    private float eje_x;
    private float eje_z;
    private bool en_suelo;

    private Rigidbody fisicas;

    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //TAL COMO EN LA CLASE 5, SOLO QUE ESTA VEZ A�ADIMOS
        //EL MOVIMIENTO EN HORIZONTAL.
        eje_x = Input.GetAxis("Horizontal");
        eje_z = Input.GetAxis("Vertical");

        // A�ADIMOS UNA COMPROBACI�N PARA SABER SI
        // EST� EN EL SUELO Y AS� PERMITIR EL SALTO:
        en_suelo = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // LA DETECCI�N DEL INPUT QUE INGRESE EL JUGADOR,
        // EN ESTE CASO JUMP PARA EL SALTO:
        if (Input.GetButtonDown("Jump") && en_suelo)
        {
            Saltar();
        }
        //COMPROBACI�N POR CONSOLA PARA SABER SI
        //EFECTICAMENTE EST� TOCANDO EL SUELO:
        Debug.Log("En suelo: " + en_suelo);
    }

    private void FixedUpdate()
    {
        //CREAMOS UN VECTOR DE MOVIMIENTO QUE COMBINA LOS EJES
        //QUE SE OBTIENEN MEDIANTE EL AXIS:
        Vector3 movimiento = new Vector3(eje_x, 0f, eje_z);

        //APLICAMOS UNA FUERZA AL OBJETO PARA QUE SE MUEVA EN LA DIRECCI�N
        //INDICADA POR EL JUGADOR A UNA VELOCIDAD DETERMINADA:
        fisicas.AddForce(movimiento * velocidad_de_movimiento);
    }

    private void Saltar()
    {
        //APLICAMOS UNA FUERZA AL SALTO PERO EN ESTE CASO:
        //LA FUERZA SE APLICA CON FORCERMODE.IMPULSE,
        //LO QUE SIGNIFICA QUE SER� UNA FUERZA INSTANT�NEA QUE
        //AGREGA UN IMPULSO INICIAL PARA EL SALTO.
        fisicas.AddForce(Vector3.up * fuerza_salto, ForceMode.Impulse);
    }
}
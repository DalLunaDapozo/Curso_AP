using UnityEngine;

public class _Moneda : MonoBehaviour
{
    
    //SCRIPT DE LAS MONEDAS DONDE SI LA AGARRAMOS, INSTANCIA UNA EXPLOSION
    //A MODO DE FEEDBACK Y PARA QUE EL JUEGO SE VEA MÁS BONITO

    //PARA PODER INSTANCIAR UNAS PARTICULAS, PRIMERO TENEMOS QUE HACER UN PREFAB Y DESPUES AGREGARLO A ESTA VARIABLE
    public GameObject particulas;
    
    //VELOCIDAD A LA QUE VA A ROTAR LA MONEDA, PARA DARLE MAS ONDA
    public float velocidad_rotacion;

    void Update()
    {
        //FUNCION PARA QUE LA MONEDA GIRE ALREDEDOR SUYO
        transform.Rotate(velocidad_rotacion, 0, 0);    
    }

    
    //UTILIZAMOS UN TRIGGER PORQUE NO QUEREMOS QUE EL JUGADOR COLISIONES CON LA MONEDA SINO ATRAVESARLA
    private void OnTriggerEnter(Collider other)
    {
        //LA FUNCION Instantiate() TIENE TRES PARÁMETROS:
        //    LO QUE VAMOS A INSTANCIAR - EN QUE POSICION - Y SU ROTACION DE INICIO
        Instantiate(particulas, transform.position, Quaternion.identity);

        //UNA VEZ INSTANCIADO LAS PARTICULAS, DESTRUIMOS LA MONEDA
        Destroy(gameObject);
    }
}

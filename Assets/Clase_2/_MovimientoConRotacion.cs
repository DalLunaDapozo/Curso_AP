using UnityEngine;

public class _MovimientoConRotacion : MonoBehaviour
{
    //USAR #region + NOMBRE, SIRVE PARA SEPARAR EL SCRIPT EN DISTINTAS SECCIONES, ES SOLO UNAS CUESTION DE ORDEN
   
    #region VARIABLES


    public float velocidad_de_movimiento;
    //EJE PARA MOVER EL PERSONAJE PARA ADELANTE
    private float eje_z;

    public float velocidad_de_rotacion;
    //EJE DINAMICO DE ROTACION
    private float eje_rotacion;
    
    //VAMOS A MOVER EL PERSONAJE CON FISICAS
    private Rigidbody fisicas;
    //HAY QUE LLAMAR AL ANIMATOR PARA PODER UTILIZAR LAS ANIMACIONES
    private Animator animaciones;
    
    //LOS #region HAY QUE CERRARLOS CON #endregion 
    #endregion

    #region FUNCIONES_UNITY
    void Start()
    {
        //LLAMAMOS LOS COMPONENTES DE RIGIDBODY Y ANIMATOR
        fisicas = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
    }

    void Update()
    {
        //HACEMOS QUE LAS DOS VARIABLES DE EJES SEAN DINAMICAS Y CAMBIEN SUS VALORES SEGÚN LOS INPUTS VERTICALES Y HORIZONTALES       
        eje_z = Input.GetAxis("Vertical");
        eje_rotacion = Input.GetAxis("Horizontal");

        //SI APRETAMOS UN INPUT PARA IR ADELANTE ACTIVAMOS LAS ANIMACIONES, SINO, LAS DESACTIVAMOS
        if (eje_z > 0)
            animaciones.SetBool("moviendose", true);
        else
            animaciones.SetBool("moviendose", false);

        //SI INTENTAMOS IR PARA ATRAS, OSEA QUE eje_z TENGA UN VALOR MENOR A CERO, HACEMOS QUE eje_z VALGA CERO
        //CON ESTO EVITAMOS QUE EL PERSONAJE VAYA PARA ATRAS
        if (eje_z < 0)
            eje_z = 0;

    }

    private void FixedUpdate()
    {
        //USAMOS AddForce() PARA MOVER NUESTRO PERSONAJE PARA ADELANTE
        fisicas.AddForce(eje_z * velocidad_de_movimiento * transform.forward);
        //USAMOS EL ROTATE() PARA ROTAR A NUESTRO PERSONAJE
        transform.Rotate(transform.rotation.x, eje_rotacion * velocidad_de_rotacion, transform.rotation.z);
    }

    #endregion
   
    
}

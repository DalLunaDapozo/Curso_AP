using UnityEngine;
using System.Collections.Generic;

public class _MovimientoLateral : MonoBehaviour
{
    //USAR #region + NOMBRE, SIRVE PARA SEPARAR EL SCRIPT EN DISTINTAS SECCIONES, ES SOLO UNAS CUESTION DE ORDEN

    #region VARIABLES

    [SerializeField] public float velocidad_de_movimiento;
    //EJE PARA MOVER EL PERSONAJE PARA ADELANTE
    private float eje_z;

    [SerializeField] private float fuerza_de_salto;
    [SerializeField] private bool en_el_suelo;

    //VAMOS A MOVER EL PERSONAJE CON FISICAS
    private Rigidbody fisicas;
    //HAY QUE LLAMAR AL ANIMATOR PARA PODER UTILIZAR LAS ANIMACIONES
    private Animator animaciones;
    private AudioSource controlador_sonidos;

    [SerializeField] private List<AudioClip> clips;

    private Transform root;

    //LOS #region HAY QUE CERRARLOS CON #endregion 
    #endregion

    #region FUNCIONES_UNITY
    void Start()
    {
        //LLAMAMOS LOS COMPONENTES DE RIGIDBODY Y ANIMATOR
        fisicas = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
        controlador_sonidos = GetComponent<AudioSource>();

        root = transform.Find("Root");
    }

    void Update()
    {
        //COMO EL MOVIMIENTO VA A SER LATERAL, QUEREMOS USAR LAS TECLAS A Y D PARA MOVER NUESTRO PERSONAJE
        //ASI QUE LE ASIGNAMOS EL EJE HORIZONTAL
        eje_z = Input.GetAxis("Horizontal");
  
        //SI APRETAMOS UN INPUT PARA IR ADELANTE ACTIVAMOS LAS ANIMACIONES, SINO, LAS DESACTIVAMOS
        if (eje_z != 0 && en_el_suelo)
            animaciones.SetBool("moviendose", true);
        else
            animaciones.SetBool("moviendose", false);
    
        if(eje_z < 0)
            root.transform.localScale = new Vector3(100, -100, 100);
        else if(eje_z > 0)
            root.transform.localScale = new Vector3(100, 100, 100);

        if (Input.GetButtonDown("Jump") && en_el_suelo)
        {
            Debug.Log("SALTAR");
            ReproducirSonido("salto");
            en_el_suelo = false;
            fisicas.AddForce(fuerza_de_salto * transform.up, ForceMode.Impulse);
        }

        animaciones.SetBool("saltando", !en_el_suelo);

    }

    private void FixedUpdate()
    {
        //USAMOS AddForce() PARA MOVER NUESTRO PERSONAJE DE COSTADO
        fisicas.AddForce(eje_z * velocidad_de_movimiento * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
            en_el_suelo = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
            en_el_suelo = false;
    }

    private void ReproducirSonido(string nombre_sonido)
    {
        int new_clip = clips.FindIndex(i => i.name == nombre_sonido);
        controlador_sonidos.clip = clips[new_clip];
        controlador_sonidos.Play();
    }

    #endregion
}

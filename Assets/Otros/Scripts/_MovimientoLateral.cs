using UnityEngine;
using System.Collections.Generic;

public class _MovimientoLateral : MonoBehaviour
{
    //USAR #region + NOMBRE, SIRVE PARA SEPARAR EL SCRIPT EN DISTINTAS SECCIONES, ES SOLO UNAS CUESTION DE ORDEN
    #region VARIABLES

    [SerializeField] public float velocidad_de_movimiento;
    //EJE PARA MOVER EL PERSONAJE PARA ADELANTE
    private float eje_z;

    //VARIABLE QUE DETERMINA LA FUERZA DE SALTO
    [SerializeField] private float fuerza_de_salto;
    //VARIABLE BOOLEANA QUE DETERMINA SI ESTAMOS EN EL SUELO O NO
    [SerializeField] private bool en_el_suelo;

    //VAMOS A MOVER EL PERSONAJE CON FISICAS
    private Rigidbody fisicas;
    //HAY QUE LLAMAR AL ANIMATOR PARA PODER UTILIZAR LAS ANIMACIONES
    private Animator animaciones;
    //ACA LLAMAMOS LA PARTE FISICA (EL MESH) PARA ROTARLO CUANDO VAMOS A LA IZQUIERDA
    private Transform root;
    
    private AudioSource controlador_sonidos;
    [SerializeField] private List<AudioClip> clips;

    //LOS #region HAY QUE CERRARLOS CON #endregion 
    #endregion

    #region FUNCIONES_UNITY
    void Start()
    {
        //LLAMAMOS LOS COMPONENTES DE RIGIDBODY Y ANIMATOR
        fisicas = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
        controlador_sonidos = GetComponent<AudioSource>();

        //A root LO BUSCAMOS ENTRE LOS OBJETOS HIJOS POR SU NOMBRE
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
    
        //SI VAMOS A LA IZQUIERDA; INVERTIMOS EL ROOT USANDO EL SCALE, SI LO PONEMOS EN NEGATIVO, LO DAMOS VUELTA
        if(eje_z < 0)
            root.transform.localScale = new Vector3(100, -100, 100);
        //SI VAMOS A LA DERECHA; LO VOLVEMOS A SU ESCALA NORMAL
        else if(eje_z > 0)
            root.transform.localScale = new Vector3(100, 100, 100);

        //ACÁ ESTAMOS LLAMANDO AL INPUT DE SALTAR, ADEMAS en_el_suelo TIENE SER VERDADERO PARA QUE PODAMOS SALTAR
        if (Input.GetButtonDown("Jump") && en_el_suelo)
        {
            //CUANDO SALTAMOS en_el_suelo DEJA DE SER VERDADERO
            en_el_suelo = false;
            //USAMOS AddForce() PARA IMPULSARNOS PARA ARRIBA CON transform.up Y USAMOS ForceMode.Impulse PARA QUE SEA UN IMPULSO Y NO UNA ACELARACIÓN
            fisicas.AddForce(fuerza_de_salto * transform.up, ForceMode.Impulse);
            //ACA VAMOS A IMPLEMENTAR EL SONIDO
            ReproducirSonido("salto");
        }

        //ESTA LINEA ESTA CHEQUEANDO TODOS LOS FRAMES QUE LA VARIABLE saltando EN EL ANIMATOR SEA LO CONTRARIO A en_el_suelo, POR ESO TIENE UN "!"
        //QUE REPRESENTA "LO CONTRARIO", ES DECIR QUE SI en_el_suelo ES VERDADERO, ENTONCES saltando VA A SER FALSO, Y VICEVERSA
        animaciones.SetBool("saltando", !en_el_suelo);
    }

    private void FixedUpdate()
    {
        //USAMOS AddForce() PARA MOVER NUESTRO PERSONAJE DE COSTADO
        fisicas.AddForce(eje_z * velocidad_de_movimiento * transform.forward);
    }

    //USAMOS OnCollisionEnter() PARA HACER QUE en_el_suelo SEA VERDADERO CUANDO ENTRAMOS EN CONTACTO CON UN TAG DE TIPO Suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            en_el_suelo = true;
            ReproducirSonido("aterrizaje");
        }
            
    }

    //USAMOS OnCollisionExit() PARA HACER QUE en_el_suelo SEA FALSO CUANDO DEJAMOS DE ESTAR EN CONTACTO CON UN TAG DE TIPO Suelo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
            en_el_suelo = false;
    }

    private void ReproducirSonido(string nombre_sonido)
    {
        int nuevo_clip = clips.FindIndex(i => i.name == nombre_sonido);
        controlador_sonidos.clip = clips[nuevo_clip];
        controlador_sonidos.Play();
    }

    #endregion
}

using UnityEngine;

public class Movimiento : MonoBehaviour
{

    #region VARIABLES

    public float velocidad_de_movimiento;
    private float eje_z;

    public float velocidad_de_rotacion;
    private float eje_rotacion;
    
    private Rigidbody fisicas;
    private Animator animaciones;

    #endregion

    #region FUNCIONES_UNITY
    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
    }

    void Update()
    {
        eje_z = Input.GetAxis("Vertical");
        eje_rotacion = Input.GetAxis("Horizontal");

        if (eje_z > 0)
            animaciones.SetBool("moviendose", true);
        else
            animaciones.SetBool("moviendose", false);

        if (eje_z < 0)
            eje_z = 0;

    }

    private void FixedUpdate()
    {
        fisicas.AddForce(eje_z * velocidad_de_movimiento * transform.forward);
        transform.Rotate(transform.rotation.x, eje_rotacion * velocidad_de_rotacion, transform.rotation.z);
    }

    #endregion
   
    
}

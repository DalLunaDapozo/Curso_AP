using UnityEngine;

public class _MovimientoTerceraPersona : MonoBehaviour
{
    //CREAMOS UNA VARIABLE PARA ALMACENAR EL RIGIDBODY
    private Rigidbody fisicas;
    //UN VECTOR 3 PARA QUE LEA LOS EJES POR LOS CUALES NOS VAMOS A MOVER (X y Z)
    private Vector3 ejes;

    //CON ESTO VAMOS A PODER CONTROLAR LA VELOCIDAD DE MOVIMIENTO
    public float velocidad_de_movimiento;
    //LO MISMO QUE ARRIBA PERO CON LA ROTACION
    public float velocidad_de_rotacion;

    //ACÁ VAMOS A CREAR LA VARIABLE DE TIPO ANIMATOR 
    private Animator animaciones;

    //INICIALIZAMOS EL RIGIDBODY Y EL ANIMATOR
    private void Start()
    {
        fisicas = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
    }

    private void Update()
    {
        //ACÁ LE DAMOS UN VALOR A LOS EJES "X" y "Z" (A LA "Y" LA DEJAMOS EN CERO)
        ejes = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //SI NOS ESTAMOS MOVIENDO
        if (ejes != Vector3.zero)
        {
            //LOS QUATERNION SON UNA MEDIDA DE ROTACION COMPLEJA A LA CUAL LE ESTAMOS DICIENDO QUE TOME EL VALOR DEL LUGAR A DONDE NOS VAMOS A MOVER
            Quaternion targetRotation = Quaternion.LookRotation(ejes.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, velocidad_de_rotacion * Time.deltaTime);

            animaciones.SetBool("moviendose", true);

        }
        else
        {
            animaciones.SetBool("moviendose", false);
        }

        //AL FINAL, MOVEMOS NUESTRO PERSONAJE CON VELOCITY
        fisicas.velocity = ejes * velocidad_de_movimiento * Time.deltaTime;
    }

}

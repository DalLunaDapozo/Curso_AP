using UnityEngine;

public class MovimientoAuto : MonoBehaviour
{
   
    public float velocidad_de_movimiento;
    private float eje_z;

    private Rigidbody fisicas;

    private Vector3 posicion_inicial;

    void Start()
    {
        posicion_inicial = transform.position;
        fisicas = GetComponent<Rigidbody>();
    }

    void Update()
    {
        eje_z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        fisicas.AddForce(eje_z * velocidad_de_movimiento * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
            VolverAlPrincipio();
    }

    private void VolverAlPrincipio()
    {
        transform.position = posicion_inicial; 
    }
}

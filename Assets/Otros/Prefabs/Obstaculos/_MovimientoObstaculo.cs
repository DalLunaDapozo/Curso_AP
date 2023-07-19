using UnityEngine;

public class _MovimientoObstaculo : MonoBehaviour
{
    
    //LA VELOCIDAD A LA QUE SE VA A MOVER
    public float velocidad_de_movimiento;

    //PARA EVITAR QUE SE MUY COMPLICADO EL SCRIPT, EN ESTA VARIABLE VAMOS A GUARDAR EL VALOR INICIAL DE velocidad_de_movimiento
    //Y VAMOS A MANEJAR ESE ÚNICO VALOR PARA PASARLO DE NEGATIVO A POSITIVO
    private float velocidad;

    //ESTA VARIABLE BOOLEANA VA SER VERDADERA O FALSA SEGUN EL RAYCAST DERECHO ENTRA EN CONTACTO CON ALGO O NO
    //PODEMOS USAR ESTA VARIABLE EN UNITY PARA DETERMINAR EL MOVIMIENTO INICIAL
    public bool derecha;
    
    //EL LAYER QUE VA A DETECTAR PARA REBOTAR
    public LayerMask layer;

    private void Start()
    {
        //ACA LE DAMOS UN VALOR A velocidad
        velocidad = velocidad_de_movimiento;
    }

    //COMO SE TRATA DE MOVIMIENTO, VAMOS A UTILIZAR EL FIXEDUPDATED
    private void FixedUpdate()
    {
        DetectarObstaculo();
        Movimiento();
    }

    //FUNCION QUE HACE QUE SE MUEVA
    private void Movimiento()
    {
        
        //DEPENDIENDO DEL VALOR DE LA VARIABLE "derecha" LA VELOCIDAD VA A SER NEGATIVA O POSITIVA
        
        
        if (derecha)
            velocidad_de_movimiento = velocidad;
        else
            velocidad_de_movimiento = -velocidad;
       
        //LUEGO VAMOS A MOVER EL OBJETO CON TRANSLATE() ----> LE VAMOS A PASAR UN Vector3.right QUE ES LO MISMO QUE ESCRIBIR Vector3(1,0,0) POR NUESTRA VELOCIDAD 
        //Y CERRAMOS CON Time.deltaTime PARA MULTIPLICARLO POR TIEMPO, ES ALGO REDUDANTE FIXEARLO ASI CUANDO ESTAMOS EN FIXEDUPDATE, PERO AL MENOS EVITAMOS USAMOS VALORES MUY BAJOS
       
        transform.Translate(Vector3.right * velocidad_de_movimiento * Time.deltaTime);
    }

    //HAY QUE DETECTAR LOS OBJETOS CON RAYCAST PORQUE ESTAMOS MOVIENDO EL OBJETO CON TRASFORM.TRANSLATE
    //UN RAYCAST ES UN RAYO INVISIBLE QUE PODEMOS DISPARAR PARA DETECTAR OTROS OBJETOS, LAS UTILIDADES QUE TIENE UN RAYCAST ES CASI INFINITA

    private void DetectarObstaculo()
    {
        //CREAMOS DOS RAYCASTHIT, UNO PARA LA DERECHA Y OTRO PARA LA IZQUIERDA
        
        RaycastHit hit_izquierda;
        RaycastHit hit_derecha;


        //LA FUNCION DE PHYSICS.RAYCAST() ES LA QUE HACE QUE DISPAREMOS UN RAYO Y PIDE VARIAS COSAS PARA FUNCIONAR CORRECTAMENTE:


        //                  UN PUNTO DE ORIGEN              UNA DIRECCION                UN RAYCASTHIT    LA DISTANCIA DE DIBUJADO   EL LAYER/S QUE DETECTA
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit_derecha, Mathf.Infinity,         layer))
        {
            
            //CON DEBUG.DRAWRAY PODEMOS DIBUJAR EL RAYO PARA PODER VERLO EN UNITY (NO SE VA A VER EN EL JUEGO)
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit_derecha.distance, Color.yellow);
            
            //PODEMOS ACCEDER A LA VARIABLE distance PARA VER LA DISTANCIA ENTRE NUESTRO OBJETO Y UN OBSTACULO, SI ESA DISTANCIA ES MENOR A 1
            //ENTONCES HACEMOS ALGO ADENTRO 
            if(hit_derecha.distance < 1f)
            {
                derecha = false;
            }
        }
        
        //ESTA PARTE ES LO MISMO QUE ARRIBA SOLO QUE CAMBIAMOS EL HIT DERECHO POR EL IZQUIERDO PARA QUE DETECTE LA DISTANCIA DEL OTRO LADO

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit_izquierda, Mathf.Infinity, layer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit_izquierda.distance, Color.blue);
            if (hit_izquierda.distance < 1f)
            {
                
                derecha = true; 
            }
        }
    }
}

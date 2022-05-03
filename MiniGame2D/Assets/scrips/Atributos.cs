using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Evita que ponga mas de un componete sobre en Objeto que tengo este script
[DisallowMultipleComponent]

//Me Agrega un componente y evita que sea el mininado desde el inspector 
[RequireComponent(typeof(BoxCollider2D))]

//evita que seleciones el hijo de un objeto para evitar cambair su posicion //me elije el padre al tocar el hijo
[SelectionBase]

//permite que el script se ejecute en el modo de edicion sin necesidad de dar play

//[ExecuteInEditMode]

public class Atributos : MonoBehaviour
{
    
    
    [HideInInspector] //evita que la variable publica se pueda ver en el inspector/pero sigue siendo publica
    public int VariablePublica;

   
    [Header("Varibles")]//me permite asiganar un titulo en el inpsector 
    [SerializeField] //Me permite que esta variable privada se pueda visualizar en el inspector.
    private int VariablePrivada;
    
    [Space(20)]//me permite poner un espacion entre las variables 

    [Range(0,100)]// me permite aseginar un rango minimo y maximo a mi variable y agregar un slaider
    [Tooltip("esta variable era un ejemplo de atributo")]//me permite poner un texto explicativo sobre las varibles o para que sirven
    public int VariblePublica2;

    [TextArea(1,5)] // me permite que con el snter se pueda spaciar la lineas de texto y le puede agregar un limite de lineas
    public string Texto;


    //me permite ejecutar una funcion-metodo desde un variable en el inpsector
    [ContextMenuItem("ejecutar un metodo desde un inten_variable", "prueba")]
    public int Pruebas;

    //te agregar un comando en el menu del inspector para ejecutar lo sigueinte
    [ContextMenu("ejecucion de metodo de prueba de el atrubuto")]
    void Prueba()
    {
        Debug.Log("prueba");
    }





}

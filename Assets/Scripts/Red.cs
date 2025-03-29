
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Red : MonoBehaviour
{
    private TextField tfResultado;
    private TextField tfNombre;
    private IntegerField tfPuntos;
    public struct DatosUsuario
    {
        public string nombre;
        public int puntos;
    }


    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        tfResultado = root.Q<TextField>("Resultado");
        print(tfResultado.value);
        tfNombre = root.Q<TextField>("Nombre");
        tfPuntos = root.Q<IntegerField>("Puntos");
        print(tfPuntos.value);

        Button botonLeer = root.Q<Button>("BotonLeer");
        botonLeer.clicked += LeerTextoPlano;

        Button botonEnviar = root.Q<Button>("BotonEnviar");
        botonEnviar.clicked += EnviarDatosJSON;
    }

    private void EnviarDatosJSON()
    {
        StartCoroutine(SubirDatosJSON());
    }

    private IEnumerator SubirDatosJSON()
    {
        DatosUsuario datos;
        datos.nombre = tfNombre.value;
        datos.puntos = tfPuntos.value;

        string datosJSON = JsonUtility.ToJson(datos);
        print(datosJSON);

        UnityWebRequest request = UnityWebRequest.Post("http://10.48.118.9:3000/unity/recibeJSON", datosJSON, "application/json");
        yield return request.SendWebRequest();

        //Después de cierto tiempo continua
        if (request.result == UnityWebRequest.Result.Success)
        {
            tfResultado.value = datosJSON + "\nEnviado correctamente\n\n";
            string respuesta = request.downloadHandler.text;
            tfResultado.value += "Respuesta\n" + respuesta;

            DatosUsuario usuario = JsonUtility.FromJson<DatosUsuario>(respuesta);
            tfResultado.value += "\nNombre: " + usuario.nombre;
            tfResultado.value += "\nPuntos: " + usuario.puntos;
        }
        else
        {
            tfResultado.value = "Error de conexión" + request.responseCode;
        }   
    }

    private void LeerTextoPlano()
    {
        StartCoroutine(DescargarTextoPlano());
    }

    private IEnumerator DescargarTextoPlano()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://10.48.118.9:3000/");
        yield return request.SendWebRequest();

        //Después de cierto tiempo continua
        if (request.result == UnityWebRequest.Result.Success)
        {
            string texto = request.downloadHandler.text;
            tfResultado.value = texto;
        }
        else
        {
            tfResultado.value = "Error de conexión" + request.responseCode;
        }

        request.Dispose();
    }
}
    

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class Red : MonoBehaviour
{
    private TextField tfResultado;
    private TextField tfNombre;
    private TextField tfPassword;
    private string horaInicio; //AGREGAMOS
    public struct DatosUsuario
    {
        public string nombre;
        public string password;
        public string horaInicio; //AGREGAMOS
        public string horaFin; //AGREGAMOS
    }


    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        tfResultado = root.Q<TextField>("Resultado");
        print(tfResultado.value);
        tfNombre = root.Q<TextField>("Nombre");
        tfPassword = root.Q<TextField>("Password");
        print(tfPassword.value);

        Button botonLeer = root.Q<Button>("BotonLeer");
        botonLeer.clicked += LeerTextoPlano;

        Button botonEnviar = root.Q<Button>("BotonEnviar");
        botonEnviar.clicked += EnviarDatosJSON;

        //AGREGAMOS
        horaInicio= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void EnviarDatosJSON()
    {
        StartCoroutine(SubirDatosJSON());
    }

    private IEnumerator SubirDatosJSON()
    {
        DatosUsuario datos;
        datos.nombre = tfNombre.value;
        datos.password = tfPassword.value;
        datos.horaInicio = horaInicio; //AGREGAMOS
        datos.horaFin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //AGREGAMOS

        string datosJSON = JsonUtility.ToJson(datos);
        print(datosJSON);

        UnityWebRequest request = UnityWebRequest.Post("http://10.48.118.9:3000/unity/recibeJSON", datosJSON, "application/json");
        yield return request.SendWebRequest();

        //Despu�s de cierto tiempo continua
        if (request.result == UnityWebRequest.Result.Success)
        {
            tfResultado.value = datosJSON + "\nEnviado correctamente\n\n";
            string respuesta = request.downloadHandler.text;
            tfResultado.value += "Respuesta\n" + respuesta;

            DatosUsuario usuario = JsonUtility.FromJson<DatosUsuario>(respuesta);
            tfResultado.value += "\nNombre: " + usuario.nombre;
            tfResultado.value += "\nPuntos: " + usuario.password;

            SceneManager.LoadScene("EscenaMenu");
        }
        else
        {
            tfResultado.value = "Error de conexi�n" + request.responseCode;
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

        //Despu�s de cierto tiempo continua
        if (request.result == UnityWebRequest.Result.Success)
        {
            string texto = request.downloadHandler.text;
            tfResultado.value = texto;
        }
        else
        {
            tfResultado.value = "Error de conexi�n" + request.responseCode;
        }

        request.Dispose();
    }
}
    
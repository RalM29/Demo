using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/*
Autores: María Fernanda Pineda Pat, Daiana Andrea Armenta Maya
Este código se encarga de controlar el menú del juego y finalizar sesión al salir.
*/

public class MenuControlador : MonoBehaviour
{
    private UIDocument menu;

    private Button botonA;
    private Button botonB;
    private Button botonC;

    // Estructura que se enviará como JSON
    [Serializable]
    public struct DatosUsuario
    {
        public string nombre;
    }

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonA = root.Q<Button>("BotonJuegoA");
        botonB = root.Q<Button>("BotonJuegoB");
        botonC = root.Q<Button>("BotonAbandonar");

        botonA.RegisterCallback<ClickEvent, String>(IniciarJuego, "SampleScene");
        botonB.RegisterCallback<ClickEvent, String>(IniciarJuego, "EscenaMapa");
        botonC.RegisterCallback<ClickEvent>(SalirDelJuego);
    }

    private void IniciarJuego(ClickEvent evt, String escena)
    {
        SceneManager.LoadScene(escena);
    }

    private void SalirDelJuego(ClickEvent evt)
    {
        Debug.Log("Cerrando juego...");
        StartCoroutine(EnviarFinSesionYSalir());
    }

    private IEnumerator EnviarFinSesionYSalir()
    {
        DatosUsuario datosUsuario;
        datosUsuario.nombre = PlayerPrefs.GetString("nombreUsuario", "usuarioAnonimo");


        string datosJSON = JsonUtility.ToJson(datosUsuario);
        Debug.Log("Enviando fin de sesión: " + datosJSON);

        // Realiza el POST al backend
        UnityWebRequest request = UnityWebRequest.Post("http://44.222.117.87:8080/sesion/end", datosJSON, "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Sesión finalizada correctamente: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error al finalizar sesión: " + request.responseCode);
        }

        Application.Quit();
    }
}


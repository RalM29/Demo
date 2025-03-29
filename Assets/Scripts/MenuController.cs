using System;
using UnityEngine;
using UnityEngine.SceneManagement; //Escena
using UnityEngine.UIElements;

/*
Autores: María Fernanda Pineda Pat, Daiana Andrea Armenta Maya
En este codigo se implemento para hacer funcionar los botones del menú de inicio del juego
*/
public class MenuControlador : MonoBehaviour
{
    private UIDocument menu; //Objeto en la escena

    private Button botonA; //Componente de la UI
    private Button botonB;
    private Button botonC;
    

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonA = root.Q<Button>("BotonJuegoA");
        botonB = root.Q<Button>("BotonJuegoB");
        botonC = root.Q<Button>("BotonAbandonar");
    
        //Callbacks
        botonA.RegisterCallback<ClickEvent, String>(IniciarJuego, "SampleScene");
        botonB.RegisterCallback<ClickEvent, String>(IniciarJuego, "EscenaMapa");
        botonC.RegisterCallback<ClickEvent>(SalirDelJuego);
    }
    private void IniciarJuego(ClickEvent evt, String escena)
    {
        SceneManager.LoadScene(escena);
    }
    private void SalirDelJuego(ClickEvent evt){
        Application.Quit();
        Debug.Log("Cerrando juego...");
    }
}

/*
    private void IniciarJuegoA(ClickEvent evt)
    {
        SceneManager.LoadScene("SampleScene");

    }
    private void IniciarJuegoB(ClickEvent evt)
    {
        SceneManager.LoadScene("EscenaMapa");

    }


}

    */
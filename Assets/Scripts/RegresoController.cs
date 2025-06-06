using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RegresoController : MonoBehaviour
{
    private UIDocument regreso;

    private Button botonRegreso;

    private void OnEnable()
    {
        regreso = GetComponent<UIDocument>();
        var root = regreso.rootVisualElement;
        botonRegreso = root.Q<Button>("botonRegreso");

        botonRegreso.RegisterCallback<ClickEvent>(Regresar);
    }

    private void Regresar(ClickEvent evt)
    {
        SceneManager.LoadScene("EscenaMenu");
    }
}

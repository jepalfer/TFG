using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
/// loadGame es una clase que se usa para cargar una partida.
/// </summary>
public class loadGame : MonoBehaviour
{
    /// <summary>
    /// Método que vuelve a la escena del menú principal.
    /// </summary>
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// Llama a <see cref="back()"/> si se pulsa el botón/tecla adecuado.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            back();
        }
        /*
        if (inputManager.GetKeyDown(inputEnum.Cancel))
        {
            back();
        }*/
    }
}

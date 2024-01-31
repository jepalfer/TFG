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
    /// M�todo que vuelve a la escena del men� principal.
    /// </summary>
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    

    /// <summary>
    /// M�todo que se ejecuta cada frame para actualizar la l�gica.
    /// Llama a <see cref="back()"/> si se pulsa el bot�n/tecla adecuado.
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

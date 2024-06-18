using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// sceneLoader es una clase que se usa para controlar cual es la siguiente escena a cargar.
/// </summary>
public class sceneLoader : MonoBehaviour
{
    /// <summary>
    /// ID de la escena a cargar.
    /// </summary>
    [SerializeField] private int _nextScene;

    /// <summary>
    /// Método principal que carga la escena al entrar en contacto con el gameObject que tiene
    /// asociado este script.
    /// </summary>
    /// <param name="collision">Colisión del gameObject que ha entrado en contacto.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprobamos que sea el jugador
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            config.getEnemiesList().Clear();
            saveSystem.saveLastScene();
            SceneManager.LoadScene(_nextScene);
        }
    }
}

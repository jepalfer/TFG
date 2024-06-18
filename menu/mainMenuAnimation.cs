using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
/// <summary>
/// mainMenuAnimation es una clase que se usa para controlar la animación del menú principal.
/// </summary>
public class mainMenuAnimation : MonoBehaviour
{
    /// <summary>
    /// Referencia al botón de iniciar partida.
    /// </summary>
    [SerializeField] private Button _newGameButton;


    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        audioManager.getInstance().GetComponent<audioManager>().getOSTPlayer().GetComponent<AudioSource>().Stop();
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo clipInfo = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
        if (stateInfo.normalizedTime >= 1.0f)
        {
            if (clipInfo.clip.name == "mainMenu")
            {
                EventSystem.current.SetSelectedGameObject(_newGameButton.gameObject);
                GetComponent<mainMenuAnimation>().enabled = false;
                audioManager.getInstance().GetComponent<audioManager>().getOSTPlayer().GetComponent<AudioSource>().Play();
            }
        }

        if (inputManager.GetKeyDown(inputEnum.accept))
        {
            GetComponent<Animator>().Play("mainMenu");
        }
    }
}

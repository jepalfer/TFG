using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// chestUI es una clase que controla la UI que aparece al pasar por un cofre.
/// </summary>
public class chestUI : MonoBehaviour
{
    /// <summary>
    /// Es la UI que aparece al pasar por delante de un cofre.
    /// </summary>
    [SerializeField] private GameObject _UI;

    /// <summary>
    /// M�todo que muestra la UI.
    /// </summary>
    public void showUI()
    {
        _UI.SetActive(true);
    }

    /// <summary>
    /// M�todo que esconde la UI.
    /// </summary>
    public void hideUI()
    {
        _UI.SetActive(false);
    }
}

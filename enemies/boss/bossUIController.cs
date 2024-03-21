using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// bossUIController es una clase que controla la UI de los jefes.
/// </summary>
public class bossUIController : MonoBehaviour
{
    /// <summary>
    /// Es la UI del jefe.
    /// </summary>
    [SerializeField] private GameObject _UI;

    /// <summary>
    /// Es el nombre en la UI del jefe.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _nameField;

    /// <summary>
    /// Es la barra de HP en la UI del jefe.
    /// </summary>
    [SerializeField] private Slider _HPBar;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        _nameField.text = GetComponent<boss>().getEnemyName();
        _UI.SetActive(true);
    }

    /// <summary>
    /// Método que modifica la barra de vida del jefe.
    /// </summary>
    /// <param name="dmg">Es el daño que le infligimos al jefe.</param>
    public void recalculateHPBar(float dmg)
    {
        float _received = dmg / GetComponent<enemy>().getEnemyData().getHealth();

        _HPBar.value -= _received;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_UI"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene la interfaz del jefe.</returns>
    public GameObject getUI()
    {
        return _UI;
    }
}

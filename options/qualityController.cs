using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// qualityController es una clase que se usa para gestionar la calidad seleccionada.
/// </summary>
public class qualityController : MonoBehaviour
{
    /// <summary>
    /// Referencia al dropdown para seleccionar la calidad.
    /// </summary>
    [SerializeField] private TMP_Dropdown _qualityDropdown;

    /// <summary>
    /// M�todo que se ejecuta al hacer visible la UI.
    /// </summary>
    public void initializeOptions()
    {
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _qualityDropdown.RefreshShownValue();
    }

    /// <summary>
    /// M�todo que modifica el nivel de calidad del juego.
    /// </summary>
    /// <param name="index">�ndice en la lista de <see cref="_qualityDropdown"/>.</param>
    public void setQualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// skillSlotLogic es una clase que se usa para los slots de habilidades equipables en la UI de equipar
/// una habilidad.
/// </summary>
public class skillSlotLogic : MonoBehaviour
{
    /// <summary>
    /// Referencia a la imagen de la habilidad.
    /// </summary>
    [SerializeField] private Image _skillSprite;

    /// <summary>
    /// Referencia a la imagen de fondo de la habilidad.
    /// </summary>
    [SerializeField] private Image _backgroundImage;

    /// <summary>
    /// Setter que modifica el sprite de <see cref="_skillSprite"/>.
    /// </summary>
    /// <param name="sprite">Sprite de la habilidad.</param>
    public void setSkillSprite(Sprite sprite)
    {
        _skillSprite.sprite = sprite;
    }


    /// <summary>
    /// Setter que modifica el sprite de <see cref="_backgroundImage"/>.
    /// </summary>
    /// <param name="color">Sprite del color.</param>
    public void setBackgroundColor(Sprite color)
    {
        _backgroundImage.sprite = color;
    }
}

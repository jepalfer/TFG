using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// skillData es una clase general que se usa para almacenar los datos "comunes" a las habilidades y de la que heredan
/// las demás clases que almacenan datos internos.
/// </summary>
[System.Serializable]
public class skillData
{
    /// <summary>
    /// Nombre de la habilidad.
    /// </summary>
    [SerializeField] protected string _skillName;

    /// <summary>
    /// Sprite de la habilidad.
    /// </summary>
    [SerializeField] protected string _skillSprite;

    /// <summary>
    /// Flag booleano que indica si la habilidad está o no desbloqueada.
    /// </summary>
    [SerializeField] protected bool _isUnlocked;

    /// <summary>
    /// Precio de la habilidad.
    /// </summary>
    [SerializeField] protected int _skillPoints;

    /// <summary>
    /// Flag booleano que indica si la habilidad puede o no ser desbloqueada.
    /// </summary>
    [SerializeField] protected bool _canBeUnlocked;

    /// <summary>
    /// ID interno de la habilidad.
    /// </summary>
    [SerializeField] protected int _skillID;
    
    /// <summary>
    /// Descripción de la habilidad.
    /// </summary>
    [TextArea(3, 10)]
    [SerializeField] protected string _skillDesc;

    /// <summary>
    /// Tipo de la habilidad.
    /// </summary>
    [SerializeField] protected skillTypeEnum _skillType;

    /// <summary>
    /// Cómo se equipa la habilidad.
    /// </summary>
    [SerializeField] protected equipEnum _equipType;

    /// <summary>
    /// Getter que devuelve <see cref="_skillName"/>.
    /// </summary>
    /// <returns><see cref="_skillName"/>.</returns>
    public string getSkillName()
    {
        return _skillName;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skillType"/>.
    /// </summary>
    /// <returns><see cref="_skillType"/>.</returns>
    public skillTypeEnum getType()
    {
        return _skillType;
    }

    /// <summary>
    /// Getter que devuelve el sprite de la habilidad cargado desde <see cref="Resources"/>.
    /// </summary>
    /// <returns>Referencia al sprite dentro de la carpeta Resources.</returns>
    public Sprite getSkillSprite()
    {
        return Resources.Load<Sprite>(_skillSprite);
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isUnlocked"/>.
    /// </summary>
    /// <returns><see cref="_isUnlocked"/>.</returns>
    public bool getIsUnlocked()
    {
        return _isUnlocked;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skillPoints"/>.
    /// </summary>
    /// <returns><see cref="_skillPoints"/>.</returns>
    public int getSkillPoints()
    {
        return _skillPoints;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canBeUnlocked"/>.
    /// </summary>
    /// <returns><see cref="_canBeUnlocked"/>.</returns>
    public bool getCanBeUnlocked()
    {
        return _canBeUnlocked;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_skillID"/>.
    /// </summary>
    /// <returns><see cref="_skillID"/>.</returns>
    public int getSkillID()
    {
        return _skillID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skillDesc"/>.
    /// </summary>
    /// <returns><see cref="_skillDesc"/>.</returns>
    public string getSkillDescription()
    {
        return _skillDesc;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_equipType"/>.
    /// </summary>
    /// <returns><see cref="_equipType"/>.</returns>
    public equipEnum getEquipType()
    {
        return _equipType;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canBeUnlocked"/>.
    /// </summary>
    /// <param name="value">Valor que indica si la habilidad puede ser desbloqueada.</param>
    public void setCanBeUnlocked(bool value)
    {
        _canBeUnlocked = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isUnlocked"/>.
    /// </summary>
    /// <param name="value">Valor que indica si la habilidad está desbloqueada.</param>
    public void setIsUnlocked(bool value)
    {
        _isUnlocked = value;
    }
}

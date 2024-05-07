using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// attributesData es una clase que se usa para guardar la información de los atributos del jugador.
/// </summary>
[System.Serializable]
public class attributesData
{
    /// <summary>
    /// Nivel del jugador.
    /// </summary>
    private int _level;

    /// <summary>
    /// Nivel de vitalidad del jugador.
    /// </summary>
    private int _vitality;

    /// <summary>
    /// Nivel de resistencia del jugador.
    /// </summary>
    private int _endurance;

    /// <summary>
    /// Nivel de fuerza del jugador.
    /// </summary>
    private int _strength;

    /// <summary>
    /// Nivel de destreza del jugador.
    /// </summary>
    private int _dexterity;

    /// <summary>
    /// Nivel de agilidad del jugador.
    /// </summary>
    private int _agility;

    /// <summary>
    /// Nivel de precisión del jugador.
    /// </summary>
    private int _precision;

    /// <summary>
    /// Constructor por defecto de la clase.
    /// </summary>
    public attributesData()
    {
        _level = statSystem.getLevel();
        _vitality = statSystem.getVitality().getLevel();
        _endurance = statSystem.getEndurance().getLevel();
        _strength = statSystem.getStrength().getLevel();
        _dexterity = statSystem.getDexterity().getLevel();
        _agility = statSystem.getAgility().getLevel();
        _precision = statSystem.getPrecision().getLevel(); 
        
    }

    /// <summary>
    /// Getter que devuelve <see cref="_level"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel del jugador.</returns>
    public int getLevel()
    {
        return _level;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_vitality"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de vitalidad del jugador.</returns>
    public int getVitality()
    {
        return _vitality;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_endurance"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de resistencia del jugador.</returns>
    public int getEndurance()
    {
        return _endurance;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_strength"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de fuerza del jugador.</returns>
    public int getStrength()
    {
        return _strength;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_dexterity"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de destreza del jugador.</returns>
    public int getDexterity()
    {
        return _dexterity;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_agility"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de agilidad del jugador.</returns>
    public int getAgility()
    {
        return _agility;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_precision"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de precisión del jugador.</returns>
    public int getPrecision()
    {
        return _precision;
    }
}

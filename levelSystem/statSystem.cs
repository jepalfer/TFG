using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// statSystem es una clase utilizada para almacenar los valores del nivel de cada atributo.
/// </summary>
public class statSystem : MonoBehaviour
{
    /// <summary>
    /// Nivel del jugador.
    /// </summary>
    private static int _level = 6;

    /// <summary>
    /// Atributo que representa la vitalidad (+ HP).
    /// </summary>
    private static attribute _vitality = new attribute();

    /// <summary>
    /// Atributo que representa la resistencia (+ stamina).
    /// </summary>
    private static attribute _endurance = new attribute();

    /// <summary>
    /// Atributo que representa la fuerza (+ daño, + daño penetrante).
    /// </summary>
    private static attribute _strength = new attribute();

    /// <summary>
    /// Atributo que representa la destreza (+ daño, + prob sangrado).
    /// </summary>
    private static attribute _dexterity = new attribute();

    /// <summary>
    /// Atributo que representa la agilidad (+ velocidad de esquiva).
    /// </summary>
    private static attribute _agility = new attribute();

    /// <summary>
    /// Atributo que representa la precisión (+ daño, + prob crítico).
    /// </summary>
    private static attribute _precision = new attribute();

    /// <summary>
    /// Setter que modifica <see cref="_level"/>.
    /// </summary>
    /// <param name="level">Nivel a asignar al jugador.</param>
    public static void setLevel(int level)
    {
        _level = level;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_level"/>.
    /// </summary>
    /// <returns><see cref="_level"/></returns>
    public static int getLevel()
    {
        return _level;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_vitality"/>.
    /// </summary>
    /// <returns><see cref="_vitality"/></returns>
    public static attribute getVitality()
    {
        return _vitality;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_endurance"/>.
    /// </summary>
    /// <returns><see cref="_endurance"/></returns>
    public static attribute getEndurance()
    {
        return _endurance;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_strength"/>.
    /// </summary>
    /// <returns><see cref="_strength"/></returns>
    public static attribute getStrength()
    {
        return _strength;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dexterity"/>.
    /// </summary>
    /// <returns><see cref="_dexterity"/></returns>
    public static attribute getDexterity()
    {
        return _dexterity;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_agility"/>.
    /// </summary>
    /// <returns><see cref="_agility"/></returns>
    public static attribute getAgility()
    {
        return _agility;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_precision"/>.
    /// </summary>
    /// <returns><see cref="_precision"/></returns>
    public static attribute getPrecision()
    {
        return _precision;
    }
}

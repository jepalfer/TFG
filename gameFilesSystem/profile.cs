using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// profile es una clase que representa internamente a los perfiles.
/// </summary>
public class profile : MonoBehaviour
{
    /// <summary>
    /// El nombre de usuario en la UI.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _userName;

    /// <summary>
    /// El nivel del perfil en la UI.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _level;

    /// <summary>
    /// El botón de cargar perfil de la UI.
    /// </summary>
    [SerializeField] private Button _loadBtn;

    /// <summary>
    /// El botón de eliminar perfil de la UI.
    /// </summary>
    [SerializeField] private Button _removeBtn;

    /// <summary>
    /// El nivel de vitalidad del perfil.
    /// </summary>
    private int _vitalityLevel;

    /// <summary>
    /// El nivel de resistencia del perfil.
    /// </summary>
    private int _enduranceLevel;

    /// <summary>
    /// El nivel de fuerza del perfil.
    /// </summary>
    private int _strengthLevel;

    /// <summary>
    /// El nivel de destreza del perfil.
    /// </summary>
    private int _dexterityLevel;

    /// <summary>
    /// El nivel de agilidad del perfil.
    /// </summary>
    private int _agilityLevel;

    /// <summary>
    /// El nivel de precisión del perfil.
    /// </summary>
    private int _precisionLevel;

    /// <summary>
    /// Setter que modifica <see cref="_level"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setLevel(int level)
    {
        _level.text = level.ToString();
    }

    /// <summary>
    /// Setter que modifica <see cref="_userName"/> asignándole un valor a su texto.
    /// </summary>
    /// <param name="name">El valor a asignar.</param>
    public void setUserName(string name)
    {
        _userName.text = name;
    }

    /// <summary>
    /// Setter que modifica <see cref="_vitalityLevel"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setVitality(int level)
    {
        _vitalityLevel = level;
    }
    /// <summary>
    /// Setter que modifica <see cref="_enduranceLevel"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setEndurance(int level)
    {
        _enduranceLevel = level;
    }
    /// <summary>
    /// Setter que modifica <see cref="_strengthLevel"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setStrength(int level)
    {
        _strengthLevel = level;
    }
    /// <summary>
    /// Setter que modifica <see cref="_dexterityLevel"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setDexterity(int level)
    {
        _dexterityLevel = level;
    }
    /// <summary>
    /// Setter que modifica <see cref="_agilityLevel"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setAgility(int level)
    {
        _agilityLevel = level;
    }
    /// <summary>
    /// Setter que modifica <see cref="_precisionLevel"/> asignándole un valor.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setPrecision(int level)
    {
        _precisionLevel = level;
    }
    
    /// <summary>
    /// Getter que devuelve el texto de <see cref="_userName"/>.
    /// </summary>
    /// <returns>Un string que contiene el nombre del perfil.</returns>
    public string getName()
    {
        return _userName.text;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_loadBtn"/>.
    /// </summary>
    /// <returns>Un botón que representa el botón de carga de la UI.</returns>

    public Button getLoadBtn()
    {
        return _loadBtn;
    }


    /// <summary>
    /// Getter que devuelve <see cref="_removeBtn"/>.
    /// </summary>
    /// <returns>Un botón que representa el botón de eliminar perfil.</returns>
    public Button getRemoveBtn()
    {
        return _removeBtn;
    }
    /// <summary>
    /// Getter que devuelve el nivel del perfil.
    /// </summary>
    /// <returns>Un int que representa el nivel del perfil.</returns>
    public int getLevel()
    {
        int level;

        if (int.TryParse(_level.text, out level))
        {

        }
        else
        {
            Debug.LogError("Fallo al convertir level a int");
        }

        return level;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_vitalityLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de vitalidad del perfil.</returns>
    public int getVitality()
    {
        return _vitalityLevel;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_enduranceLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de resistencia del perfil.</returns>
    public int getEndurance()
    {
        return _enduranceLevel;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_strengthLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de fuerza del perfil.</returns>
    public int getStrength()
    {
        return _strengthLevel;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_dexterityLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de destreza del perfil.</returns>
    public int getDexterity()
    {
        return _dexterityLevel;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_agilityLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de agilidad del perfil.</returns>
    public int getAgility()
    {
        return _agilityLevel;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_precisionLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel de precisión del perfil.</returns>
    public int getPrecision()
    {
        return _precisionLevel;
    }
}

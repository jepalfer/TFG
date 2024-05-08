using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// treeController es una clase auxiliar que se usa para la lógica de los árboles de habilidades.
/// </summary>
public class treeController : MonoBehaviour
{
    /// <summary>
    /// Referencia a la habilidad inicial del árbol.
    /// </summary>
    [SerializeField] private GameObject _initialSkill;

    /// <summary>
    /// Getter que devuelve <see cref="_initialSkill"/>.
    /// </summary>
    /// <returns><see cref="_initialSkill"/>.</returns>
    public GameObject getInitialSkill()
    {
        return _initialSkill;
    }
}

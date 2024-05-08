using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sceneSwitch es una clase que se usa para controlar la posición en la que aparece el jugador 
/// dependiendo de la escena desde la que haya cambiado de escena.
/// </summary>
public class sceneSwitch : MonoBehaviour
{
    /// <summary>
    /// ID de la escena por la que se accede desde arriba.
    /// </summary>
    [SerializeField] private int _topAccessID;

    /// <summary>
    /// Coordenadas en las que el jugador aparece si se ha accedido desde arriba.
    /// </summary>
    [SerializeField] private List<float> _topAccessCoordinates;

    /// <summary>
    /// ID de la escena por la que se accede desde la derecha.
    /// </summary>
    [SerializeField] private int _rightAccessID;

    /// <summary>
    /// Coordenadas en las que el jugador aparece si se ha accedido desde la derecha.
    /// </summary>
    [SerializeField] private List<float> _rightAccessCoordinates;

    /// <summary>
    /// ID de la escena por la que se accede desde abajo.
    /// </summary>
    [SerializeField] private int _bottomAccessID;

    /// <summary>
    /// Coordenadas en las que el jugador aparece si se ha accedido desde abajo.
    /// </summary>
    [SerializeField] private List<float> _bottomAccessCoordinates;

    /// <summary>
    /// ID de la escena por la que se accede desde la izquierda.
    /// </summary>    
    [SerializeField] private int _leftAccessID;

    /// <summary>
    /// Coordenadas en las que el jugador aparece si se ha accedido desde la izquierda.
    /// </summary>
    [SerializeField] private List<float> _leftAccessCoordinates;

    /// <summary>
    /// Coordenadas en las que aparece el jugador como spawn inicial.
    /// </summary>
    [SerializeField] private List<float> _spawnPointCoordinates;

    /// <summary>
    /// Getter que devuelve <see cref="_spawnPointCoordinates"/>.
    /// </summary>
    /// <returns><see cref="_spawnPointCoordinates"/>.</returns>
    public List<float> getSpawnPoint()
    {
        return _spawnPointCoordinates;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_topAccessCoordinates"/>.
    /// </summary>
    /// <returns><see cref="_topAccessCoordinates"/>.</returns>
    public List<float> getTopCoordinates()
    {
        return _topAccessCoordinates;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_rightAccessCoordinates"/>.
    /// </summary>
    /// <returns><see cref="_rightAccessCoordinates"/>.</returns>
    public List<float> getRightCoordinates()
    {
        return _rightAccessCoordinates;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_bottomAccessCoordinates"/>.
    /// </summary>
    /// <returns><see cref="_bottomAccessCoordinates"/>.</returns>
    public List<float> getBottomCoordinates()
    {
        return _bottomAccessCoordinates;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_leftAccessCoordinates"/>.
    /// </summary>
    /// <returns><see cref="_leftAccessCoordinates"/>.</returns>
    public List<float> getLeftCoordinates()
    {
        return _leftAccessCoordinates;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_topAccessID"/>.
    /// </summary>
    /// <returns><see cref="_topAccessID"/>.</returns>
    public int getTopID()
    {
        return _topAccessID;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_rightAccessID"/>.
    /// </summary>
    /// <returns><see cref="_rightAccessID"/>.</returns>
    public int getRightID()
    {
        return _rightAccessID;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_bottomAccessID"/>.
    /// </summary>
    /// <returns><see cref="_bottomAccessID"/>.</returns>
    public int getBottomID()
    {
        return _bottomAccessID;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_leftAccessID"/>.
    /// </summary>
    /// <returns><see cref="_leftAccessID"/>.</returns>
    public int getLeftID()
    {
        return _leftAccessID;
    }
}

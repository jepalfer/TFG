using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// soulContainerController es una clase utilizada para controlar el comportamiento del gameObject que aparece al morir el jugador
/// y que contiene sus almas.
/// </summary>
public class soulContainerController : MonoBehaviour
{
    /// <summary>
    /// La cantidad de almas internas del objeto.
    /// </summary>
    private long _souls;

    /// <summary>
    /// La posici�n en la que aparece el objeto
    /// </summary>
    private float [] _position;

    /// <summary>
    /// Datos sobre el contenedor de almas.
    /// </summary>
    private soulContainerData _containerData;

    /// <summary>
    /// Booleano para saber cu�ndo el jugador se encuenta en el contenedor.
    /// </summary>
    private bool _playerOn;


    /// <summary>
    /// Setter que modifica <see cref="_souls"/>.
    /// </summary>
    /// <param name="value">La cantidad de almas a asignar.</param>
    public void setSouls(long value)
    {
        _souls = value;
    }

    /// <summary>
    /// M�todo que modifica la posici�n en la que aparece la entidad.
    /// </summary>
    /// <param name="position">Array de floats que contiene la posici�n del objeto.</param>
    public void setPosition(float[] position)
    {
        transform.position = new Vector3(position[0], position[1], position[2]);
    }

    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns>Un long que contiene el n�mero de almas a devolver.</returns>
    public long getSouls()
    {
        return _souls;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_position"/>.
    /// </summary>
    /// <returns>Un array de floats que contiene la posici�n del objeto.</returns>
    public float[] getPosition()
    {
        return _position;
    }

    /// <summary>
    /// M�todo que da al jugador las almas correspondientes y destruye el objeto.
    /// </summary>
    public void recollectSouls()
    {
        config.getPlayer().GetComponent<combatController>().receiveSouls(getSouls());
        saveSystem.saveSoulContainerData(false);
        Destroy(gameObject);
    }

    /// <summary>
    /// M�todo para saber cu�ndo entramos en el bc del objeto.
    /// </summary>
    /// <param name="collision">Colisi�n del objeto entrante.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            _playerOn = true;
        }
    }

    /// <summary>
    /// M�todo para saber cu�ndo salimos de la bc del objeto.
    /// </summary>
    /// <param name="collision">Colisi�n del objeto saliente.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playerMovement>() != null)
        {
            _playerOn = false;
        }
    }

    /// <summary>
    /// M�todo que se ejecuta al iniciar el script. Inicializa distintos datos.
    /// </summary>
    private void Start()
    {
        
        _containerData = saveSystem.loadSoulContainerData();
        setSouls(_containerData.getSouls());
        setPosition(_containerData.getPosition());
    }

    /// <summary>
    /// M�todo que se ejecuta en cada frame para actualizar la l�gica.
    /// </summary>
    private void Update()
    {
        if (_playerOn && inputManager.GetKeyDown(inputEnum.interact))
        {
            recollectSouls();
        }
    }
}

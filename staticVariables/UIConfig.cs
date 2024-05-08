using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIConfig es una clase que se usa para almacenar las referencias al controlador de UI y de hogueras.
/// </summary>
public static class UIConfig
{
    /// <summary>
    /// Referencia al controlador de las UI.
    /// </summary>
    private static UIController _controller;

    /// <summary>
    /// Referencia al controlador de las hogueras.
    /// </summary>
    private static bonfireBehaviour _bonfire;

    /// <summary>
    /// Setter que modifica la referencia a <see cref="_controller"/>.
    /// </summary>
    /// <param name="controller">Referencia al controlador.</param>
    public static void setController(UIController controller)
    {
        _controller = controller;
    }

    /// <summary>
    /// Setter que modifica la referencia a <see cref="_bonfire"/>.
    /// </summary>
    /// <param name="bonfire">Referencia al controlador.</param>
    public static void setBonfire(bonfireBehaviour bonfire)
    {
        _bonfire = bonfire;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_controller"/>.
    /// </summary>
    /// <returns><see cref="_controller"/>.</returns>
    public static UIController getController()
    {
        return _controller;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_bonfire"/>.
    /// </summary>
    /// <returns><see cref="_bonfire"/>.</returns>
    public static bonfireBehaviour getBonfire()
    {
        return _bonfire;
    }
}

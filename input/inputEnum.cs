using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// inputEnum es un enum para acceder a los distintos inputs de una forma c�moda y �ptima.
/// </summary>
public enum inputEnum
{
    /// <summary>
    /// key para saltar.
    /// </summary>
    jump,

    /// <summary>
    /// key para utilizar la UI de subida de nivel.
    /// </summary>
    right,

    /// <summary>
    /// key para utilizar la UI de subida de nivel.
    /// </summary>
    left,

    /// <summary>
    /// key para moverse hacia abajo.
    /// </summary>
    down,
    /// <summary>
    /// key para moverse hacia arriba.
    /// </summary>
    up,

    /// <summary>
    /// key para golpear con el arma primaria.
    /// </summary>
    primaryAttack,

    /// <summary>
    /// key para golpear con el arma secundaria.
    /// </summary>
    secundaryAttack,
    
    /// <summary>
    /// key para mostrar el minimapa.
    /// </summary>
    showMinimap,
    
    /// <summary>
    /// key para esquivar.
    /// </summary>
    roll,
    
    /// <summary>
    /// key para pausar el juego.
    /// </summary>
    pause,
    
    /// <summary>
    /// key para interactuar con distintos elementos.
    /// </summary>
    interact,
    
    /// <summary>
    /// key para navegar entre pesta�as de algunos men�s.
    /// </summary>
    previous,
    
    /// <summary>
    /// key para navegar entre pesta�as de algunos men�s.
    /// </summary>
    next,
    
    /// <summary>
    /// key para entrar a ciertos men�s.
    /// </summary>
    enterEquip,
    
    /// <summary>
    /// key para salir de ciertos men�s.
    /// </summary>
    cancel,
    
    /// <summary>
    /// key para aceptar algunos cambios.
    /// </summary>
    accept,
    
    /// <summary>
    /// key para navegar entre los objetos equipados (anterior).
    /// </summary>
    previousItem,

    /// <summary>
    /// key para navegar entre los objetos equipados (siguiente).
    /// </summary>
    nextItem,
    
    /// <summary>
    /// key para usar el objeto equipado seleccionado.
    /// </summary>
    useItem,
    
    /// <summary>
    /// key para a�adir un objeto a la selecci�n para comprar.
    /// </summary>
    oneMoreItem,
    
    /// <summary>
    /// key para eliminar un objeto de la selecci�n para comprar.
    /// </summary>
    oneLessItem,
    
    /// <summary>
    /// key para confirmar la selecci�n de objetos a comprar.
    /// </summary>
    buyItem,
}


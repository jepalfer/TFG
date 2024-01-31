using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class soulsData
{
    private long _souls;
    public soulsData()
    {
        if (long.TryParse(generalUIConfiguration.getAlmas().text, out _souls))
        {
        }
        else
        {
            // No se pudo convertir el texto a long para 'requiredSouls'
            Debug.LogError("Error al convertir requiredSouls a long");
        }
    }

    public long getSouls()
    {
        return _souls;
    }
}

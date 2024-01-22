using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class generalUIConfiguration
{
    private static TextMeshProUGUI _almas;

    public static void setAlmas(TextMeshProUGUI field)
    {
        _almas = field;
    }
    public static TextMeshProUGUI getAlmas()
    {
        return _almas;
    }
}

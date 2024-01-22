using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIConfig
{

    private static UIController _controller;
    private static bonfireBehaviour _bonfire;

    public static void setController(UIController controller)
    {
        _controller = controller;
    }

    public static void setBonfire(bonfireBehaviour bonfire)
    {
        _bonfire = bonfire;
    }

    public static UIController getController()
    {
        return _controller;
    }
    public static bonfireBehaviour getBonfire()
    {
        return _bonfire;
    }
}

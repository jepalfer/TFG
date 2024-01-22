using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[System.Serializable]

public class inputData
{

    private Dictionary<string, KeyCode> _inputList;
    private Dictionary<string, ButtonControl> _inputListgamepad;
    public inputData()
    {
        _inputList = inputManager.getInputList();
        _inputListgamepad = inputManager.getGamepadInputs();
    }

    public Dictionary<string, KeyCode> getInputList()
    {
        return _inputList;
    }
    public Dictionary<string, ButtonControl> getGamepadInputs()
    {
        return _inputListgamepad;
    }
}

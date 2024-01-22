using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LUManager : MonoBehaviour
{
    public GameObject _UI;
    private static bool _isLevelingUp = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _UI.SetActive(!_UI.activeSelf);
            _isLevelingUp = !_isLevelingUp;
            Time.timeScale = _isLevelingUp ? 0f : 1f;
        }
    }

    public static bool getLevelingUp()
    {
        return _isLevelingUp;
    }
}

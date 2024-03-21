using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class playerData
{
    private float[] _position;
    private bool _isFacingRight;
    public playerData()
    {
        _position = new float[3];

        _position[0] = config.getPlayer().transform.position.x;
        _position[1] = config.getPlayer().transform.position.y;
        _position[2] = config.getPlayer().transform.position.z;

        _isFacingRight = config.getPlayer().GetComponent<playerMovement>().getIsFacingRight();
    }

    public float getX()
    {
        return _position[0];
    }
    public float getY()
    {
        return _position[1];
    }
    public float getZ()
    {
        return _position[2];
    }

    public bool getIsFacingRight()
    {
        return _isFacingRight;
    }
}

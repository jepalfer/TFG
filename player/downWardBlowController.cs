using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downWardBlowController : MonoBehaviour
{
    private bool _isInDownWardBlow;
    private heightEnum _blowHeight;
    private float _critDamage;
    private float _penetratingDamage;
    private float _baseDamage;
    private bool _canCreateExpansive;
    [SerializeField] private GameObject _expansivePrefab;
    private void Awake()
    {
        _canCreateExpansive = false;
        _critDamage = 0f;
        _penetratingDamage = 0f;
        _baseDamage = 0f;
    }

    private void Start()
    {
        _blowHeight = heightEnum.none;
    }
    public void setIsInDownWardBlow(bool value)
    {
        _isInDownWardBlow = value;
    }

    public bool getIsInDownWardBlow()
    {
        return _isInDownWardBlow;
    }

    public heightEnum getAttackHeight()
    {
        return _blowHeight;
    }

    public void setAttackHeight(heightEnum height)
    {
        _blowHeight = height;
    }

    private void Update()
    {
        if (config.getPlayer().GetComponent<collisionController>().getIsGrounded() || config.getPlayer().GetComponent<collisionController>().getIsOnLadderTop() ||
            config.getPlayer().GetComponent<collisionController>().getIsOnOneWay() || config.getPlayer().GetComponent<collisionController>().getIsOnSlope())
        {
            if (_canCreateExpansive && _blowHeight == heightEnum.strong)
            {
                Vector2 leftHit = new Vector2(transform.position.x - (GetComponent<playerMovement>().getBoxCollider().size.x / 2), transform.position.y - (GetComponent<playerMovement>().getBoxCollider().size.y / 2));
                Vector2 rightHit = new Vector2(transform.position.x + (GetComponent<playerMovement>().getBoxCollider().size.x / 2), transform.position.y - (GetComponent<playerMovement>().getBoxCollider().size.y / 2));
                /*
                Instantiate(_expansivePrefab, leftHit, Quaternion.identity);
                Instantiate(_expansivePrefab, rightHit, Quaternion.identity);*/
            }
            _blowHeight = heightEnum.none;
            GetComponent<downWardBlowController>().setIsInDownWardBlow(false);
        }
    }

    public void setCanCreateExpansive(bool value)
    {
        _canCreateExpansive = value;
    }

    public void addDamages(float critDamage, float penDamage, float baseDamage)
    {
        _critDamage += critDamage;
        _baseDamage += baseDamage;
        _penetratingDamage += penDamage;
    }
}

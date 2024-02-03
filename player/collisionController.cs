using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionController : MonoBehaviour
{
    private Vector2 _suelo;
    private Vector2 _lados;
    private Vector3 _newSize;
    private Vector3 _newCenter;
    private float _diff;
    [SerializeField] private Transform _groundCheckCollider;
    [SerializeField] private Transform _headCheckCollider;
    [SerializeField] private Transform _sideCheckCollider;
    [SerializeField] private bool _hitHead;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _Side;
    [SerializeField] private bool _isOnLadderTop;
    [SerializeField] private bool _isOnOneWay;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _oneWayGround;
    [SerializeField] private LayerMask _ladder;
    [SerializeField] private LayerMask _ladderTop;
    [SerializeField] private LayerMask _slope;
    [SerializeField] private float _slopeDistance;

    private BoxCollider2D _bc;
    private Vector2 _colliderSize;
    [SerializeField] private Vector2 _slopeNormalPerpendicular;
    [SerializeField] private float _slopeDownAngle;
    [SerializeField] private float _slopeDownAngleOld;
    [SerializeField] private bool _isOnSlope;
    private float _slopeSideAngle;
    [SerializeField] private bool _canCheckSlope = true;
    // Start is called before the first frame update
    void Awake()
    {
        _suelo = new Vector2(GetComponent<BoxCollider2D>().size.x / 1.5f, 0.1f);
        _lados = new Vector2(0.1f, GetComponent<BoxCollider2D>().size.y - 0.2f);

        _bc = GetComponent<BoxCollider2D>();
        _colliderSize = _bc.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.getIsInPauseUI() && !UIController.getIsInEquippingSkillUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && 
            !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsSelectingSkillUI() && !UIController.getIsInShopUI())
        {
            if (GetComponent<combatController>().getPrimaryWeapon() != null || GetComponent<combatController>().getSecundaryWeapon() != null)
            {
                bool condicion = true;
                if (GetComponent<combatController>().getPrimaryWeapon() != null)
                {
                    condicion = !GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getIsAttacking();
                }
                if (GetComponent<combatController>().getSecundaryWeapon() != null)
                {
                    condicion = !GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getIsAttacking() && condicion;
                }
                if (condicion)
                {
                    headCheck();
                    oneWayCheck();
                    SideCheck();
                    GroundCheck();
                    ladderTopCheck();
                    slopeCheck();
                }
                else
                {
                    _isOnSlope = false;
                    _hitHead = false;
                    _isGrounded = false;
                    _Side = false;
                    _isOnLadderTop = false;
                }
            }
            else
            {
                headCheck();
                oneWayCheck();
                SideCheck();
                GroundCheck();
                ladderTopCheck();
                slopeCheck();
            }
        }
           
    }


    //COLLIDERS CHECK

    private void slopeCheck()
    {
        if (_canCheckSlope)
        {
            Vector2 checkPos = transform.position - new Vector3(0f, _colliderSize.y / 2 - _bc.offset.y);
            slopeCheckHorizontal(checkPos);
            slopeCheckVertical(checkPos);
        }
    }

    private void slopeCheckVertical (Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeDistance / 2.5f, _slope);

        if (hit)
        {
            _slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _slopeDownAngleOld || _slopeDownAngle == 0.0f) //Es completamente horizontal
            {
                _isOnSlope = true;
            }

            _slopeDownAngleOld = _slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    private void slopeCheckHorizontal (Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeDistance, _slope);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeDistance, _slope);

        if (slopeHitFront)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }
    }

    public void setCanCheckSlope(bool value)
    {
        _canCheckSlope = value;
    }

    public void setIsOnSlope(bool value)
    {
        _isOnSlope = value;
    }

    private void SideCheck()
    {
        _Side = false;
        _Side = Physics2D.OverlapBox(_sideCheckCollider.position, _lados, 1f, _ground);

    }

    private void headCheck()
    {
        _hitHead = false;
        _hitHead = Physics2D.OverlapBox(_headCheckCollider.position, _suelo, 1f, _ground);

    }
    private void GroundCheck()
    {
        _isGrounded = false;
        _isGrounded = Physics2D.OverlapBox(_groundCheckCollider.position, _suelo, 1f, _ground);
    }
    private void oneWayCheck()
    {
        _isOnOneWay = false;
        _isOnOneWay = Physics2D.OverlapBox(_groundCheckCollider.position, _suelo, 0f, _oneWayGround);
    }
    
    private void ladderTopCheck()
    {
        _isOnLadderTop = false;
        _isOnLadderTop = Physics2D.OverlapBox(_groundCheckCollider.position, _suelo, 0f, _ladderTop);
    }

    public bool getIsGrounded()
    {
        return _isGrounded;
    }

    public bool getIsOnLadderTop()
    {
        return _isOnLadderTop;
    }

    public bool getIsOnOneWay()
    {
        return _isOnOneWay;
    }

    public bool getHithead()
    {
        return _hitHead;
    }

    public bool getSide()
    {
        return _Side;
    }

    public bool getIsOnSlope()
    {
        return _isOnSlope;
    }

    public Vector2 getSlopeNormalPerpendicular()
    {
        return _slopeNormalPerpendicular;
    }
    public float getSlopeDownAngle()
    {
        return _slopeDownAngle;
    }
    public float getSlopeDownAngleOld()
    {
        return _slopeDownAngleOld;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckCollider.position, _suelo);
        Gizmos.DrawWireCube(_headCheckCollider.position, _suelo);
        Gizmos.DrawWireCube(_sideCheckCollider.position, _lados);
    }
}

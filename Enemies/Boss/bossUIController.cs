using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class bossUIController : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private Slider _HPBar;

    private void Start()
    {
        _nameField.text = GetComponent<boss>().getEnemyName();
        _UI.SetActive(true);
    }
    public void recalculateHPBar(float dmg)
    {
        float _received = dmg / GetComponent<enemy>().getHealth();

        _HPBar.value -= _received;
    }
}

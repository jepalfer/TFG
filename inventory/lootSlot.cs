using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// lootSlot es una clase que nos permite visualizar con claridad qué objetos recibimos de loot.
/// </summary>
public class lootSlot : MonoBehaviour
{
    /// <summary>
    /// El tiempo que el prefab ha estado en pantalla.
    /// </summary>
    private float _timeInScreen = 0f;

    /// <summary>
    /// El tiempo máximo que el prefab puede estar en pantalla.
    /// </summary>
    private float _maxTimeInScreen = 2f;

    /// <summary>
    /// El sprite del prefab.
    /// </summary>
    [SerializeField] private Image _lootSprite;

    /// <summary>
    /// El nombre del prefab.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _lootText;

    /// <summary>
    /// La cantidad recibida.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _lootQuantity;
        
    /// <summary>
    /// Método que se llama al crear el prefab. Inicializa <see cref="_timeInScreen"/>.
    /// </summary>
    private void Start()
    {
        _timeInScreen = 0f;
    }

    public void setSprite(Sprite sprite)
    {
        _lootSprite.sprite = sprite;
    }

    public void setText(string text)
    {
        _lootText.text = text;
    }

    public void setLootQuantity(int quantity)
    {
        _lootQuantity.text = "x" + quantity.ToString();
    }

    /// <summary>
    /// Método que se llama cada frame para actualizar la lógica. Destruye el prefab si <see cref="_timeInScreen"/> supera a <see cref="_maxTimeInScreen"/>.
    /// </summary>
    public void Update()
    {
        if (_timeInScreen >= _maxTimeInScreen)
        {
            Destroy(gameObject);
        }

        _timeInScreen += Time.deltaTime;
    }
}

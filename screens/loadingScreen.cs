using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// loadingScreen es una clase que se usa para controlar el contenido de la pantalla de carga.
/// </summary>
public class loadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _itemSprite;

    /// <summary>
    /// Método que se ejecuta al inicio del script.
    /// </summary>
    private void Start()
    {
        config.setLoadingScreen(gameObject);
        config.getPlayer().GetComponent<playerMovement>().enabled = false;
        config.getPlayer().GetComponent<combatController>().enabled = false;
        config.getAudioManager().GetComponent<audioManager>().getOSTPlayer().GetComponent<AudioSource>().Stop();
        //int index = Random.Range(0, config.getPlayer().GetComponent<equippedInventory>().getAllItems().Count - 1);
        int getFromWeapons = Random.Range(0, 2);
        int index;
        if (getFromWeapons == 1)
        {
            index = Random.Range(0, config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Count);
            _itemSprite.sprite = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[index].getWeapon().GetComponent<weapon>().getIcon();
            _nameText.text = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[index].getWeapon().GetComponent<weapon>().getName();
            _descriptionText.text = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[index].getWeapon().GetComponent<weapon>().getDesc();
        }
        else
        {
            index = Random.Range(0, config.getPlayer().GetComponent<equippedInventory>().getAllItems().Count);
            _itemSprite.sprite = config.getPlayer().GetComponent<equippedInventory>().getAllItems()[index].GetComponent<generalItem>().getIcon();
            _nameText.text = config.getPlayer().GetComponent<equippedInventory>().getAllItems()[index].GetComponent<generalItem>().getName();
            _descriptionText.text = config.getPlayer().GetComponent<equippedInventory>().getAllItems()[index].GetComponent<generalItem>().getDesc();
        }



        if (config.getEnemiesList() != null)
        {
            for (int i = 0; i < config.getEnemiesList().Count; ++i)
            {
                config.getEnemiesList()[i].GetComponent<enemyController>().enabled = false;
                config.getEnemiesList()[i].GetComponent<enemyStateMachine>().enabled = false;
            }
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica
    /// </summary>
    void Update()
    { 
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            config.getPlayer().GetComponent<playerMovement>().enabled = true;
            config.getPlayer().GetComponent<combatController>().enabled = true;
            if (config.getEnemiesList() != null)
            {
                for (int i = 0; i < config.getEnemiesList().Count; ++i)
                {
                    config.getEnemiesList()[i].GetComponent<enemyController>().enabled = true;
                    config.getEnemiesList()[i].GetComponent<enemyStateMachine>().enabled = true;
                }
            }
            config.getAudioManager().GetComponent<audioManager>().getOSTPlayer().GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// scrollController es una clase que se usa para controlar el scroll de un rect.
/// Código original: https://gist.github.com/mandarinx/eae10c9e8d1a5534b7b19b74aeb2a665
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class scrollController : MonoBehaviour
{
    /// <summary>
    /// Velocidad a la que se mueve el scroll.
    /// </summary>
    private float _scrollSpeed = 10f;

    /// <summary>
    /// Lista de seleccionables.
    /// </summary>
    private List<Selectable> _mSelectables = new List<Selectable>();
    
    
    /// <summary>
    /// Referencia al scroll que vamos a modificar.
    /// </summary>
    private ScrollRect _mScrollRect;

    /// <summary>
    /// Próxima posición del scroll.
    /// </summary>
    private Vector2 _mNextScrollPosition = Vector2.up;

    /// <summary>
    /// Método que comprueba los inputs realizados.
    /// </summary>
    void inputScroll()
    {
        if (_mSelectables.Count > 0)
        {
            if (inputManager.GetKeyDown(inputEnum.up))
            {
                scrollToSelected(false);
            }
            else if (inputManager.GetKeyDown(inputEnum.down))
            {
                scrollToSelected(false);
            }
            else if (inputManager.GetKeyDown(inputEnum.useItem))
            {
                scrollToSelected(false);
            }
            else if (inputManager.GetKeyDown(inputEnum.oneMoreItem))
            {
                scrollToSelected(false);
            }
            else
            {
                scrollToSelected(true);
            }
        }
    }

    /// <summary>
    /// Método que calcula la nueva posición del scroll.
    /// </summary>
    /// <param name="quickScroll">Booleano para calcular posiciones.</param>
    void scrollToSelected(bool quickScroll)
    {
        int selectedIndex = -1;
        Selectable selectedElement = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;

        if (selectedElement)
        {
            selectedIndex = _mSelectables.IndexOf(selectedElement);
        }
        if (selectedIndex > -1)
        {
            if (quickScroll)
            {
                Debug.Log("ENTRO QUICKSCROLL");
                _mScrollRect.normalizedPosition = new Vector2(0, 1 - (selectedIndex / ((float)_mSelectables.Count - 1)));
                //Debug.Log("_mScrollRect.normalizedPosition: " + _mScrollRect.normalizedPosition);
                _mNextScrollPosition = _mScrollRect.normalizedPosition;
                Debug.Log("True ==> Selected Index: " + selectedIndex + " || Speed: " + _scrollSpeed + " || _mNextScrollPosition: " + _mNextScrollPosition + " || _mScrollRect.normalizedPosition: " + _mScrollRect.normalizedPosition +
                    " || Lerp: " + Vector2.Lerp(_mScrollRect.normalizedPosition, _mNextScrollPosition, _scrollSpeed * Time.deltaTime));
            }
            else
            {
                _mNextScrollPosition = new Vector2(0, 1 - (selectedIndex / ((float)_mSelectables.Count - 1)));
                Debug.Log("False ==> Selected Index: " + selectedIndex + " || Speed: " + _scrollSpeed + " || _mNextScrollPosition: " + _mNextScrollPosition + " || _mScrollRect.normalizedPosition: " + _mScrollRect.normalizedPosition +
                    " || Lerp: " + Vector2.Lerp(_mScrollRect.normalizedPosition, _mNextScrollPosition, _scrollSpeed * Time.deltaTime));
            }
        }
    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    mouseOver = true;
    //}
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    mouseOver = false;
    //    ScrollToSelected(false);
    //}


    /// <summary>
    /// Método que se ejecuta al inicio tras <see cref="Awake()"/>.
    /// </summary>
    void Start()
    {
        //Obtenemos el scroll
        _mScrollRect = GetComponent<ScrollRect>();
        Debug.Log("_mScrollRecr: " +_mScrollRect);
        //Si hay scroll
        if (_mScrollRect)
        {
            //Obtenemos todos los seleccionables
            _mScrollRect.content.GetComponentsInChildren(_mSelectables);
        }

        //Seleccionamos el scroll
        scrollToSelected(true);
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>    
    void Update()
    {
        // Scroll via input.
        inputScroll();
        _mScrollRect.normalizedPosition = Vector2.Lerp(_mScrollRect.normalizedPosition, _mNextScrollPosition, _scrollSpeed * Time.deltaTime);
    }

}

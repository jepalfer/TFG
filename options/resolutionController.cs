using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// resolutionController es una clase que se usa para gestionar las distintas resoluciones para el juego.
/// </summary>
public class resolutionController : MonoBehaviour
{
    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolutions;

    private int _currentRefreshRate;
    private int _currentResolutionIndex = 0;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    // Start is called before the first frame update
    void Start()
    {
        _resolutions = Screen.resolutions;
        _filteredResolutions = new List<Resolution>();


        _resolutionDropdown.ClearOptions();
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < _resolutions.Length; ++i)
        {
            if (_resolutions[i].refreshRate == _currentRefreshRate)
            {
                _filteredResolutions.Add(_resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < _filteredResolutions.Count; ++i)
        {
            string resolutionOption = _filteredResolutions[i].width + "x" + _filteredResolutions[i].height + " - " + _filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution res = _filteredResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, true);
    }

    public TMP_Dropdown getResolutionDropdown()
    {
        return _resolutionDropdown;
    }
}

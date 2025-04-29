using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Localization.Settings;
public class ControladorDeidiomas : MonoBehaviour
{

    private bool _active = false;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        
    }

    public void ChangeLocale(int localeID)
    {
        if (_active)
        {

            return;
            
        }
        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocalKey", localeID);
        _active = false;
    }

}

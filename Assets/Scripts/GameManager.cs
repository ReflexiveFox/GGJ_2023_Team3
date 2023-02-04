#define DEBUG
//#undef DEBUG
using System;
using UnityEngine;
//using EndlessWarfare.SettingValues;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RootBoy
{
    public class GameManager : MonoBehaviour
    {
        //public static event Action<SettingsValues> OnSettingsChanged = delegate { };

        public static GameManager instance;

        //[Header("Game settings")]
        //[SerializeField] private SettingsValues _settingsValues;

        //public SettingsValues SettingsValues
        //{
        //    get => _settingsValues;
        //    set
        //    {
        //        if (_settingsValues == value)
        //        {
        //            return;
        //        }

        //        _settingsValues = value;
        //    }
        //}

        private void Awake()
        { 
            // if the istance exists and it's not us
            if (instance != null && instance != this)
            {
                //destroy this instance 
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                // don't destroy the game manager (mainly to keep the mouseControls toggle) while changing the levels (aka scenes)
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
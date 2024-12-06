using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OSLoader
{
    [CreateAssetMenu(fileName = "Prefabs")]
    internal class MenuPrefabs : ScriptableObject
    {
        public GameObject canvas;
        public GameObject loaderMenu;
        public GameObject modEntry;

        public GameObject header;

        public GameObject stringSetting;
        public GameObject intSetting;
        public GameObject intSliderSetting;
        public GameObject floatSetting;
        public GameObject floatSliderSetting;
        public GameObject boolSetting;
        public GameObject keybindSetting;
        public GameObject enumSetting;
    }
}

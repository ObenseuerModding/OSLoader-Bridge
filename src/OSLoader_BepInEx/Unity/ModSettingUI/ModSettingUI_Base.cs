using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace OSLoader
{
    internal abstract class ModSettingUI_Base : MonoBehaviour
    {
        public TMP_Text title;
        public ModEntryUI modEntryUI;

        public abstract void OnInitialize(ModSettingDrawer relatedDrawer);

        public abstract void OnSave();
    }
}

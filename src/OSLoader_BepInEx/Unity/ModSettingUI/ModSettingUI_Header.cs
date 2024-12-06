using OSLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TMPro;

namespace OSLoader
{
    internal class ModSettingUI_Header : ModSettingUI_Base
    {
        public TMP_Text header;

        public override void OnInitialize(ModSettingDrawer relatedDrawer)
        {
            header.text = ((SettingsHeaderAttribute)relatedDrawer.relatedAttribute).name;
        }

        public override void OnSave()
        {
            throw new NotImplementedException("This should never be called, something went wrong...");
        }
    }
}

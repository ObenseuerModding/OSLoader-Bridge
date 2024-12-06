using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    [Flags]
    public enum KeybindConstraints
    {
        None =                  0b0000000,
        Default =               0b0010010,

        NoFunctions =           0b0000001,
        //NoEscape =              0b0000010,
        NoExistingGameBinds =   0b0000100,
        NoExistingModBinds =    0b0001000,
        NoDefaultSteamBinds =   0b0010000,
        //NoDoubleClicks =        0b0100000,
        //NoTripleClicks =        0b1000000,

        NoExistingBinds =       0b0011100
    }
}

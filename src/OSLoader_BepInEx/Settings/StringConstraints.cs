using System;
using System.Collections.Generic;
using System.Text;

namespace OSLoader
{
    [Flags]
    public enum StringConstraints
    {
        None =              0b000000,

        NoSpaces =          0b000001,
        NoTrim =            0b000010,
        NoAlphas =          0b000100,
        NoNumerics =        0b001000,
        NoSpecials =        0b010000,
        NoEmpty =           0b100000,

        OnlyAlphas =        0b011001,
        OnlyNumeric =       0b010101,
        OnlyAlphaNumeric =  0b010001
    }
}

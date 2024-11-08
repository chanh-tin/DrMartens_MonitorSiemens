using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Common.Enums
{
    public enum EnumValidationRules
    {
        MinLength = 1,
        MaxLength = 2,
        NoNumber = 3,
        HasNumber = 4,
        HasLowerCase = 5,
        HasUpperCase = 6,
        NoSpecial = 7,
        HasSpecial = 8,
        ConfirmPassword = 9,
    }
}

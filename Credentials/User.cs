using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Credentials
{
    internal static class User
    {
        internal const string validUsername = "standard_user";
        internal const string validPassword = "secret_sauce";

        internal const string invalidUsername = "standard_user_123";
        internal const string invalidPassword = "secret_sauce_123";

        internal const string emptyUsername = "";
        internal const string emptyPassword = "";

        internal const string whiteSpacesUsername = "         ";
        internal const string whiteSpacesPassword = "            ";

        internal const string validFirstName = "Svetoslav";
        internal const string validLastName = "Savov";
        internal const string validZipCode = "1000";
    }
}

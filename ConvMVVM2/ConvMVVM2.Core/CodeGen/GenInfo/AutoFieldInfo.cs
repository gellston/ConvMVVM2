using System.Collections.Generic;

namespace ConvMVVM2.Core.CodeGen.GenInfo
{
    internal struct AutoFieldInfo
    {
        internal string Identifier;
        internal string TypeName;
        internal List<string> TargetNames = new List<string>();

        #region Constructor
        public AutoFieldInfo()
        {

        }
        #endregion
    }
}

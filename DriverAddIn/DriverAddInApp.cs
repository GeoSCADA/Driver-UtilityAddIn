using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using ClearSCADA.DriverFramework;
using System.Threading;

namespace DriverAddIn
{
    public class DriverAddInApp : DriverApp
    {
        #region Constants

        private const string LogSource = "AddIn Driver: ";
        private const string GlobalNameSpace = @"Global\";
        private const int SingleInstanceErrorCode = 1000;

        #endregion

        #region Object Lifetime

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        #endregion


    }
}

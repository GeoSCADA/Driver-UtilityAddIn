using System;
using System.Collections.Generic;
using System.Text;
using ClearSCADA.DBObjFramework; 
using ClearSCADA.DriverFramework;
using AddIn;

namespace DriverAddIn
{
    public static class DriverAddIn
    {
        public static void Main(string[] commandLineArguments)
        {
            using (DriverAddInApp app = new DriverAddInApp())
            {
                if (app.Init(new AddInModule(), commandLineArguments))
                {
                    app.MainLoop();
                }
            }
        }
    }
}

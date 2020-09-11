using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using ClearSCADA.DBObjFramework;
using ClearSCADA.DriverFramework;
using AddIn;

namespace DriverAddIn
{
    public class DriverScanner : DriverScanner<AddInScanner>
    {
        #region Constants

        private const string LogSource = "AddIn Scanner: ";
        private const string IntervalIndexNamePart = "Interval";
        private const string OffsetIndexNamePart = "Offset";
        private const string ManualAnaloguePointTypeName = "ManualAnalogue";
        private const string TariffAnaloguePointTypeName = "TariffAnalogue";

        #endregion

        private readonly Dictionary<uint, PointSourceEntry> m_manualPoints = new Dictionary<uint, PointSourceEntry>();

        #region Properties
        public DriverAddInApp Application { get { return (DriverAddInApp)this.App; } }

        #endregion

        #region Scanner Events

        public override SourceStatus OnDefine()
        {
            App.LogApp(true, LogSource + "OnDefine started...");
            // Configure the scanner
            SetScanRate((uint)(DBScanner.NormalScanRate * 1000), DBScanner.NormalScanOffset, true);

            // Add a separate list of Manual points
            lock (m_manualPoints)
            {
                m_manualPoints.Clear();
                foreach (PointSourceEntry point in Points)
                {
                    if (IsManualAnaloguePoint(point))
                    {
                        Log("Adding " + point.FullName + "...");
                        uint id = (uint)(int)(point.DatabaseObject["Id"]);
                        m_manualPoints.Add(id, point);
                    }
                }
            }

            App.LogApp(true, LogSource + "OnDefine ...ended");
            return SourceStatus.Online;
        }

        public override void OnScan()
        {
            App.LogApp(true, LogSource + "OnScan started...");
            // Update tariff point values
            foreach (PointSourceEntry point in Points)
            {
                if (IsTariffAnaloguePoint(point))
                {
                    uint id = (uint)(int)(point.DatabaseObject["Id"]);
                    // Call code in the Module to get the value
                    object Reply = null;
                    App.SendReceiveObject(DBScanner.Id, AddIn.OPC.OPC_AddIn_GetTariffValueNow, id, ref Reply);

                    point.SetValue(DateTime.UtcNow, PointSourceEntry.Quality.Good, PointSourceEntry.Reason.EndofPeriod, (double) Reply);
                }
            }
            FlushUpdates();

            App.LogApp(true, LogSource + "OnScan ...ended");
        }

        public override void OnExecuteAction(DriverTransaction transaction)
        {

            switch (transaction.ActionType)
            {

                case (uint)OPC.AddInScannerDriverActions.ProcessValue:
                    App.LogApp(true, LogSource + "Processing 'Process Value' driver action...");
                    // Process Manual Value
                    {
                        uint pointId = (uint)(transaction.get_Args(0));
                        DateTime time = (DateTime)(transaction.get_Args(1));
                        PointSourceEntry.Quality quality = (PointSourceEntry.Quality)(transaction.get_Args(2));
                        double value = (double)(transaction.get_Args(3));
                        PointSourceEntry.Reason reason = (PointSourceEntry.Reason)(transaction.get_Args(4));

                        bool success = SetPointValue(pointId, time, quality, value, reason);
                        CompleteTransaction(transaction, (success ? 1 : 0), (success ? "Success" : "Failed"));
                    }
                    break;
                default:
                    base.OnExecuteAction(transaction);
                    break;
            }
        }

        #endregion


        #region Point Queries

        private bool IsManualAnaloguePoint(PointSourceEntry point)
        {
            return ((string)(point.DatabaseObject["TypeName"]) == ManualAnaloguePointTypeName);
        }

        private bool IsTariffAnaloguePoint(PointSourceEntry point)
        {
            return ((string)(point.DatabaseObject["TypeName"]) == TariffAnaloguePointTypeName);
        }

        #endregion

        #region Manual Point

        private bool SetPointValue(uint pointId, 
            DateTime time, 
            PointSourceEntry.Quality quality, 
            double value, 
            PointSourceEntry.Reason reason)
        {
            bool success = false;

            lock (m_manualPoints)
            {
                try
                {
                    PointSourceEntry p;
                    if (m_manualPoints.TryGetValue(pointId, out p))
                    {
                        p.SetValue(time, quality, reason, value);
                        success = true;

                        App.LogApp(true,"SetPointValue: Setting " + pointId.ToString() +
                            " Time: " + time.ToString() +
                            " Quality: " + quality.ToString() +
                            " Value: " + value);

                        // We are not updating the point in OnScan so flush here
                        FlushUpdates();
                    }
                    else
                    {
                        App.LogError(1, "SetPointValue: Failed to find Manual Point with ID " + pointId.ToString());
                    }
                }
                catch (Exception e)
                {
                    Log("Exception: " + e.Message);
                }
            }

            if (!success)
            {
                Log("Point Set Failed for Manual Point with ID " + pointId.ToString());
            }

            return success;
        }

        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STELLAR.Console
{
    public class Tab2DbConsoleEngine : ConsoleEngineBase
    {
        protected override void PreProcess()
        {
            this.ReadInput = false;
            this.showTimings = true;
        }

        protected override void PostProcess()
        {

            //Do the TAB2DB import (tab delimited files)
            Arguments a = new Arguments(this.Arguments);
            String dbFileName = a["db"].Trim().ToLower();
            String fileName = a["tab"].Trim();
            String tableName = a["table"] == null ? "" : a["table"].Trim().ToLower();
            bool hasHeader = a["noheader"] == null ? true : false;
            //char delimiter = a["delimiter"] == null ? ',' : a["delimiter"].PadRight(1,',').ToCharArray(0,1)[0];
            this.Out.WriteLine("Importing file '{0}' to db '{1}'", System.IO.Path.GetFileName(fileName), dbFileName);
            try
            {
                int rowCount = STELLAR.Data.API.Delimited2DB(dbFileName, fileName, '\t', hasHeader);
                this.Out.WriteLine("{0} rows imported", rowCount);
            }
            catch (Exception ex)
            {
                this.Error.WriteLine(ex.Message);
            }
        }

        protected override string Usage()
        {
            return String.Format("tab2db /db:\"NAME\" /tab:\"FILE\" [/table:\"NAME\"] [/noheader]");
        }

        protected override bool ValidateArguments()
        {
            Arguments a = new Arguments(this.Arguments);
            if (a["db"] == null)
                return false;
            if (a["tab"] == null)
                return false;
            return true;
        }
    }
}
﻿/*
================================================================================
Creator : Ceri Binding, University of Glamorgan
Project	: STELLAR
Classes	: STELLAR.Console.CommandCSV2XML
Summary	: Handler for STELLAR console command
License : http://creativecommons.org/licenses/by/3.0/ 
================================================================================
History :

12/01/2011  CFB Created classes
================================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STELLAR.Console
{
    class CommandCSV2XML : CommandBase 
    {
        protected override void PreProcess()
        {
            this.ReadInput = false;
            this.showTimings = true;
        }

        protected override void PostProcess()
        {
            //Do the CSV2XML conversion
            Arguments a = new Arguments(this.Arguments);
            String csvFileName = a["csv"].Trim();
            String xmlFileName = a["xml"] == null ? "" : a["xml"].Trim();             
            bool hasHeader = a["noheader"] == null ? true : false;
            this.Out.WriteLine("Converting '{0}' to XML", System.IO.Path.GetFileName(csvFileName));
            
            try
            {
                int rowCount = STELLAR.Data.API.Delimited2XML(csvFileName, xmlFileName, ',', hasHeader);
                this.Out.WriteLine("{0} rows exported", rowCount);
            }
            catch (Exception ex)
            {
                this.Error.WriteLine(ex.Message);
            }
        }

        protected override string Usage()
        {
            return String.Format("csv2xml /csv:\"FILE\" [/xml:\"FILE\"] [/noheader]");
        }

        protected override bool ValidateArguments()
        {
            Arguments a = new Arguments(this.Arguments);
            if (a["csv"] == null)
                return false;
            return true;          
        }
    }
}

﻿#region about
/*
================================================================================
Creator : Ceri Binding, University of Glamorgan
Project	: STELLAR
Classes	: STELLAR.Console.CommandXML2RDF
Summary	: Handler for STELLAR console command
License : http://creativecommons.org/licenses/by/3.0/ 
================================================================================
History :

12/01/2011  CFB Created classes
================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STELLAR.Console
{
    public class CommandXML2RDF : CommandBase
    {
        protected override void PreProcess()
        {
            this.ReadInput = false;
            this.showTimings = true;
        }

        protected override void PostProcess()
        {
            Arguments a = new Arguments(this.Arguments);
            String xmlFileName = a["xml"].Trim();
            String templateName = a["template"].Trim();
            String rdfFileName = a["rdf"] == null ? "" : a["rdf"].Trim();            
            String namespaceURI = a["ns"] == null ? "" : a["ns"].Trim();
            this.Out.WriteLine("Converting '{0}' to RDF using template '{1}'", System.IO.Path.GetFileName(xmlFileName), templateName);
               
            try
            {
                STELLAR.Data.API.XML2RDF(xmlFileName, templateName, rdfFileName, namespaceURI);
            }            
            catch (Exception ex)
            {
                this.Error.WriteLine(ex.Message);
                //System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }

        protected override string Usage()
        {
            return String.Format("xml2rdf /xml:\"FILE\" /template:\"NAME\" [/rdf:\"FILE\"] [/ns:\"URI\"]");
        }

        protected override bool ValidateArguments()
        {
            Arguments a = new Arguments(this.Arguments);
            if (a["xml"] == null)
                return false;
            if (a["template"] == null)
                return false;
            return true;
        }
    }
}

﻿//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerShellConnectorDocumenter.cs" company="Microsoft">
//      Copyright (c) Microsoft. All Rights Reserved.
//      Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>
// <summary>
// MIM Configuration Documenter
// </summary>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace MIMConfigDocumenter
{
    using System;
    using System.Collections.Specialized;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI;
    using System.Xml.Linq;
    using System.Xml.XPath;

    /// <summary>
    /// The PowerShellConnectorDocumenter documents the configuration of a PowerShell connector.
    /// </summary>
    internal class PowerShellConnectorDocumenter : Extensible2ConnectorDocumenter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellConnectorDocumenter"/> class.
        /// </summary>
        /// <param name="pilotXml">The pilot configuration XML.</param>
        /// <param name="productionXml">The production configuration XML.</param>
        /// <param name="connectorName">The name.</param>
        /// <param name="configEnvironment">The environment in which the config element exists.</param>
        public PowerShellConnectorDocumenter(XElement pilotXml, XElement productionXml, string connectorName, ConfigEnvironment configEnvironment)
            : base(pilotXml, productionXml, connectorName, configEnvironment)
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.ReportFileName = Documenter.GetTempFilePath(this.ConnectorName + ".tmp.html");
                this.ReportToCFileName = Documenter.GetTempFilePath(this.ConnectorName + ".TOC.tmp.html");
            }
            finally
            {
                Logger.Instance.WriteMethodExit();
            }
        }

        /// <summary>
        /// Gets the active directory connector configuration report.
        /// </summary>
        /// <returns>
        /// The Tuple of configuration report and associated TOC
        /// </returns>
        public override Tuple<string, string> GetReport()
        {
            Logger.Instance.WriteMethodEntry();

            try
            {
                this.WriteConnectorReportHeader();

                this.ProcessConnectorProperties();

                this.ProcessExtensible2ExtensionInformation();
                this.ProcessExtensible2ConnectivityInformation();

                this.ProcessExtensible2ConnectorCapabilities();
                this.ProcessExtensible2GlobalParameters();
                this.ProcessConnectorProvisioningHierarchyConfiguration();
                this.ProcessExtensible2PartitionsAndHierarchiesConfiguration();

                this.ProcessConnectorSelectedObjectTypes();
                this.ProcessConnectorSelectedAttributes();
                this.ProcessExtensible2AnchorConfigurations();
                this.ProcessConnectorFilterRules();
                this.ProcessConnectorJoinAndProjectionRules();
                this.ProcessConnectorAttributeFlows();
                this.ProcessConnectorDeprovisioningConfiguration();
                this.ProcessConnectorExtensionsConfiguration();
                this.ProcessConnectorRunProfiles();

                return this.GetReportTuple();
            }
            finally
            {
                Logger.Instance.WriteMethodExit();

                // Clear Logger call context items
                Logger.ClearContextItem(ConnectorDocumenter.LoggerContextItemConnectorName);
                Logger.ClearContextItem(ConnectorDocumenter.LoggerContextItemConnectorGuid);
                Logger.ClearContextItem(ConnectorDocumenter.LoggerContextItemConnectorCategory);
            }
        }
    }
}

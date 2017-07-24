// ----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/08/13">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Configuration;

    /// <summary>
    /// Global control of web service.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        [SuppressMessage("Methods and properties that don't access instance data should be static", "S2325")]
        protected void Application_Start(object sender, EventArgs e)
        {
            DbSettingProviderManager.Current.Start();

            log4net.Config.XmlConfigurator.Configure();
            log4net.LogManager.GetLogger("GeneralLog").Info("Start");
        }
    }
}
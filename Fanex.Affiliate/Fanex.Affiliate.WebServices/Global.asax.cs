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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using Fanex.Data.Configuration;

    /// <summary>
    /// Global control of web service.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        [SuppressMessage("SonarQube", "S2325:Methods and properties that don't access instance data should be static", Justification = "Reviewed")]
        protected void Application_Start(object sender, EventArgs e)
        {
            DbSettingProviderManager.Current.Start();

            log4net.Config.XmlConfigurator.Configure();
            log4net.LogManager.GetLogger("GeneralLog").Info("Start");
        }
    }
}
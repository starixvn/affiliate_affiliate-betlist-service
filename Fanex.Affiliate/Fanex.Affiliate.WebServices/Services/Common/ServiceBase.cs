// ----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/16">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Services
{
    using System;
    using Fanex.Logging;

    /// <summary>
    /// Service base.
    /// </summary>
    public class ServiceBase
    {
        protected void HandleException(Exception exception)
        {
            LogService.Error(exception.Message, exception);
        }
    }
}
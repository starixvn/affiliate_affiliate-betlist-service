// ----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

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
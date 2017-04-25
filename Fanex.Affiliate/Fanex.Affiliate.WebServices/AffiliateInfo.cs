// ----------------------------------------------------------------------
// <copyright file="AffiliateInfo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices
{
    using System.Xml.Serialization;

    /// <summary>
    /// Affiliate Information.
    /// </summary>
    [XmlRoot("AffiliateInformation")]
    public class AffiliateInfo
    {
        public AffiliateInfo()
        {
            ReturnCode = 0;
            ReturnMessage = "Successfully";
        }

        public AffiliateInfo(int errorCode, string errorMessage)
        {
            ReturnCode = errorCode;
            ReturnMessage = errorMessage;
        }

        [XmlElement("ReturnCode")]
        public int ReturnCode { get; set; }

        [XmlElement("ReturnMessage")]
        public string ReturnMessage { get; set; }
    }
}
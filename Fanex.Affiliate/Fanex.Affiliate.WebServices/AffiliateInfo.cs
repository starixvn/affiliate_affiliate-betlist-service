// ----------------------------------------------------------------------
// <copyright file="AffiliateInfo.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/03">Created</change>
//   <change who="" date=""></change>
// </history>

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
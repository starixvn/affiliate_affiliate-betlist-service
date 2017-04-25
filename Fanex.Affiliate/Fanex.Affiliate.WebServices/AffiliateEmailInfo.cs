// ----------------------------------------------------------------------
// <copyright file="AffiliateEmailInfo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices
{
    using System.Xml.Serialization;

    /// <summary>
    /// Affiliate Email Information.
    /// </summary>
    [XmlRoot("AffiliateInformation")]
    public class AffiliateEmailInfo : AffiliateInfo
    {
        [XmlElement("FullName")]
        public string Fullname { get; set; }

        [XmlElement("UserName")]
        public string Username { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }
    }
}
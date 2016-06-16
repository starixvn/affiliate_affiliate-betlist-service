// ----------------------------------------------------------------------
// <copyright file="AffiliateEmailInfo.cs" company="Nexcel Solutions Vietnam">
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
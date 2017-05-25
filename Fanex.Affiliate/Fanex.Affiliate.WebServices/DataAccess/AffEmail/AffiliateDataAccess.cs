// ----------------------------------------------------------------------
// <copyright file="AffiliateDataAccess.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DataAccess.AffEmail
{
    using Criteria.AffEmail;
    using Data;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class AffiliateDataAccess : IAffiliateDataAccess
    {
        /// <summary>
        /// Get Affiliate Information.
        /// </summary>
        /// <param name="criteria">Affiliate Information criteria.</param>
        /// <param name="siteName">Site Name.</param>
        /// <returns>Affiliate Information.</returns>
        public object GetAffiliateInfo(AffiliateInfoCriteria criteria, string siteName)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey(siteName)))
            {
                var param = criteria.GetSpParams();
                var dataSet = objectDb.ExecuteDataSet(param);

                return int.Parse(param["errorcode"].ToString()) == 0
                    ? new AffiliateEmailInfo
                    {
                        ReturnCode = int.Parse(param["errorcode"].ToString()),
                        Username = dataSet.Tables[0].Rows[0]["username"].ToString(),
                        Fullname = dataSet.Tables[0].Rows[0]["fullname"].ToString(),
                        Email = dataSet.Tables[0].Rows[0]["email"].ToString()
                    }
                    : new AffiliateInfo
                    {
                        ReturnCode = int.Parse(param["errorcode"].ToString()),
                        ReturnMessage = param["errormsg"].ToString()
                    };
            }
        }
    }
}
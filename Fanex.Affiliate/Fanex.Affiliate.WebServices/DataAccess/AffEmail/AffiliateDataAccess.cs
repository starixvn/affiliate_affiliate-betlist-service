// ----------------------------------------------------------------------
// <copyright file="AffiliateDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/24">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using Criteria;
    using Data;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class AffiliateDataAccess : IAffiliateDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateDataAccess" /> class.
        /// </summary>
        public AffiliateDataAccess()
        {
        }

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
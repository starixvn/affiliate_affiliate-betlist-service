// ----------------------------------------------------------------------
// <copyright file="BetListResultsCriteria.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Criteria
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Data.Repository;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class BetListResultsCriteria : CriteriaBase
    {
        [Required]
        public int MatchId { get; set; }

        [Required]
        public int Bettype { get; set; }

        [Required]
        public string BetId { get; set; }

        public int? RaceNo { get; set; }

        [Required]
        public int SportType { get; set; }

        public int? IsOutRight { get; set; }

        public long? RefNo { get; set; }

        public DateTime? WinLossDate { get; set; }

        public string UserName { get; set; }
    }
}
// ----------------------------------------------------------------------
// <copyright file="BetType.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/19">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Enumerations
{
    using System.Diagnostics.CodeAnalysis;
    
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed.")]
    public enum BetType
    {
        None = 0,
        Handicap = 1,
        OddEven = 2,
        OverUnder = 3,
        CorrectScore = 4,
        Ft_1X2 = 5,
        TotalGoal = 6,
        FirstHalfHandicap = 7,
        FirstHalfOverUnder = 8,
        MixParlay = 9,
        CombinationMixParlay = 29,
        Outright = 10,
        TotalCorners = 11,
        FirstHalfOddEven = 12,
        CleanSheet = 13,
        FirstGoalLastGoal = 14,
        FirstHalf_1X2 = 15,
        HtFt = 16,
        SecondHalfHdp = 17,
        SecondHalfOverUnder = 18,
        Substitutes = 19,
        MoneyLine = 20,
        NextGoal = 22,
        NextCorner = 23,
        Win = 31,
        Place = 32,
        WinPlace = 33,
        WinUkTote = 41,
        PlaceUkTote = 42,
        WinPlaceUkTote = 43,

        CasinoGames = 71,
        P2PGames = 72,
        MiniGames = 73,
        Bal = 100,
        Dep = 101,
        Wit = 102,
        Trac = 103,
        Traa = 104,
        Tram = 105,
        Tram1 = 106,
        DDEP = 107,
        DWIT = 108,
        DCAN = 109,
        DBON = 110,
        DTRA = 111,
        CSTRA = 112,
        CSPD = 113,
        P2PD = 114,
        DTRATP = 115,
        DCAN3rd = 116,
        CSMGTRA = 117,
        CSMGPD = 118,
        NG1stBallOU = 81,
        NgLastBallOu = 82,
        Ng1StBallOe = 83,
        NgLastBallOe = 84,
        NgOuFt = 85,
        NgOeFt = 86,
        NgHighLow = 87,
        NgWarrior = 88,
        NgNextCombo = 89,
        NgNumberGroup = 90
    }
}
module Leagues
open Scoring
open Analysis

type League =
    {
        Name: string
        Scoring: Scoring
        Roster: Roster
    }

let auctionScoring = 
    {
        PassYd = 0.04
        PassTd = 4
        Int = -2
        RushYd = 0.1
        RushTd = 6
        PPR = true
        RecYd = 0.1
        RecTd = 6
    }
let auctionRoster = 
    {
        Quarterbacks = 1
        RunningBacks = 2
        WideReceivers = 2
        TightEnds = 1
        FlexRBWR = 1
        FlexRBWRTE = 0
        NumTeams = 12
    }
let auctionLeague = { Name="auction"; Scoring=auctionScoring; Roster=auctionRoster }

let carolinaScoring =
    {
        PassYd = 0.04
        PassTd = 4
        Int = -1
        RushYd = 0.1
        RushTd = 6
        PPR = false
        RecYd = 0.1
        RecTd = 6
    }
let carolinaRoster = 
    {
        Quarterbacks = 1
        RunningBacks = 2
        WideReceivers = 2
        TightEnds = 1
        FlexRBWR = 0
        FlexRBWRTE = 1
        NumTeams = 10
    }
let carolinaLeague = { Name="Carolina"; Scoring=carolinaScoring; Roster=carolinaRoster }
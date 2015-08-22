module Scoring
open Shared

type Scoring =
    {
        PassYd: float
        PassTd: int
        Int: int
        RushYd: float
        RushTd: int
        PPR: bool
        RecYd: float
        RecTd: int
    }

let scorePassing scoring (passingStats:PassingStats option) =
    match passingStats with
    | Some stats -> 
        stats.PassYd * scoring.PassYd
            + stats.PassTd * (float scoring.PassTd)
            + stats.Int * (float scoring.Int)
    | None -> 0.0

let scoreRushing scoring (rushingStats:RushingStats option) =
    match rushingStats with
    | Some stats ->
        stats.RushTd * (float scoring.RushTd)
            + stats.RushYd * scoring.RushYd
    | None -> 0.0

let scoreReceiving scoring (receivingStats:ReceivingStats option) =
    match receivingStats with
    | Some stats ->
        stats.RecNum * (if scoring.PPR then 1.0 else 0.0)
            + stats.RecTd * (float scoring.RecTd)
            + stats.RecYd * scoring.RecYd
    | None -> 0.0

let scorePlayer scoring player =
    scorePassing scoring player.PassingStats
        + scoreRushing scoring player.RushingStats
        + scoreReceiving scoring player.ReceivingStats
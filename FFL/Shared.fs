module Shared

type PassingStats =
    {
        PassYd: float
        PassTd: float
        Int: float
    }

type RushingStats =
    {
        RushYd: float
        RushTd: float
    }

type ReceivingStats =
    {
        RecNum: float
        RecYd: float
        RecTd: float
    }

type Player =
    {
        Name: string
        Position: string
    }

type PlayerStats = 
    {
        Player: Player
        PassingStats: PassingStats option
        RushingStats: RushingStats option
        ReceivingStats: ReceivingStats option
    }
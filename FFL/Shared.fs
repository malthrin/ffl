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

type PlayerStats = 
    {
        Name: string
        Position: string
        PassingStats: PassingStats option
        RushingStats: RushingStats option
        ReceivingStats: ReceivingStats option
    }
module Analysis
open Shared

type Roster =
    {
        Quarterbacks: int
        RunningBacks: int
        WideReceivers: int
        TightEnds: int
        FlexRBWR: int
        FlexRBWRTE: int
        NumTeams: int
    }

let calculateMagicNumbers roster =
    [
        ("QB", float roster.Quarterbacks * float roster.NumTeams)
        ("RB", (float roster.RunningBacks + 0.5 * float roster.FlexRBWR + 0.4 * float roster.FlexRBWRTE) * float roster.NumTeams)
        ("WR", (float roster.WideReceivers + 0.5 * float roster.FlexRBWR + 0.4 * float roster.FlexRBWRTE) * float roster.NumTeams)
        ("TE", (float roster.TightEnds + 0.2 * float roster.FlexRBWRTE) * float roster.NumTeams)
    ] |> List.map (fun (pos,num)->(pos, (int num)-1)) |> Map.ofList
    
let thrd (_,_,x) = x

type PlayerScore =
    {
        Player: Player
        Score: float
        ScoreOverBaseline: float
        ScoreOverNext: float
    }

let scoreOverNext (player, nextPlayer) =
    {
        Player = player.Player
        Score = player.Score
        ScoreOverBaseline = player.ScoreOverBaseline
        ScoreOverNext = player.Score - nextPlayer.Score
    }

let calculateScoreOverNext players =
    let emptyPlayer = { Player={ Name=""; Position="" }; Score=0.0; ScoreOverBaseline=0.0; ScoreOverNext=0.0 }
    Seq.zip (emptyPlayer::players) players
        |> Seq.map scoreOverNext
        |> Seq.skip 1
        |> List.ofSeq

let calculateMarginalValue roster (players:(PlayerStats*float) seq) =
    let magicNumbers = calculateMagicNumbers roster
    players
        |> Seq.groupBy (fun (player,score)-> player.Player.Position)
        |> Seq.map 
            (fun (position, players) ->                
                let sorted = List.ofSeq players |> List.sortBy snd |> List.rev 
                let baselineScore = snd sorted.[magicNumbers.[position]]
                sorted
                    |> List.map (fun (player, score) -> (player.Player, score))
                    |> List.map (fun (player,score)-> { Player=player; Score=score; ScoreOverBaseline=score-baselineScore; ScoreOverNext=0.0 })
                    |> calculateScoreOverNext)
        |> List.concat
        |> List.sortBy (fun player -> player.ScoreOverBaseline)
        |> List.rev
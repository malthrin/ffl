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
let calculateMarginalValue roster (players:(PlayerStats*float) seq) =
    let magicNumbers = calculateMagicNumbers roster
    players
        |> Seq.groupBy (fun (player,score)-> player.Position)
        |> Seq.map 
            (fun (position, players) ->                
                let sorted = List.ofSeq players |> List.sortBy snd |> List.rev 
                let baselineScore = snd sorted.[magicNumbers.[position]]
                List.map (fun (player,score)-> (player, score, score-baselineScore)) sorted)
        |> List.concat
        |> List.sortBy thrd
        |> List.rev
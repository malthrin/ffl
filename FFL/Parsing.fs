module Parsing
open Shared
open FileIO
open System

let double s = Double.Parse s

let parsePassing (values:string array) =
    {
        PassYd = values.[2] |> double
        PassTd = values.[3] |> double
        Int = values.[4] |> double
    }

let parseRushing (values:string array) =
    {
        RushYd = values.[1] |> double
        RushTd = values.[3] |> double
    }

let parseReceiving (values:string array) =
    {
        RecNum = values.[0] |> double
        RecYd = values.[1] |> double
        RecTd = values.[3] |> double
    }

type Position =
    {
        Position: string
        PassingStats: (int*int) option
        RushingStats: (int*int) option
        ReceivingStats: (int*int) option
    }

let positions =
    [
        { Position="QB"; PassingStats = Some(1,7); RushingStats = Some(8,12); ReceivingStats = None }
        { Position="RB"; PassingStats = None; RushingStats = Some(1,4); ReceivingStats = Some(5,8) }
        { Position="WR"; PassingStats = None; RushingStats = None; ReceivingStats = Some(1,4) }
        { Position="TE"; PassingStats = None; RushingStats = None; ReceivingStats = Some(1,4) }
    ]

let parseStats columns parser (values:string array) =
    match columns with 
    | Some(first,last) -> Some (parser values.[first..last])
    | None -> None

let parsePositionRow position (row:string) =
    let values = row.Split [| ',' |]
    {
        Name = values.[0]
        Position = position.Position
        PassingStats = parseStats position.PassingStats parsePassing values
        RushingStats = parseStats position.RushingStats parseRushing values
        ReceivingStats = parseStats position.ReceivingStats parseReceiving values
    }

let loadPositionFile dataPath position = FileIO.loadFile (dataPath + "fantasyfootball-stats_weeklyprojections_"+position.Position+"_season.csv")

let parsePositionFile dataPath position =
    let contents = loadPositionFile dataPath position
    (contents.Split [| '\n' |]).[2..]
        |> Seq.filter (fun s-> s.Length>0)
        |> Seq.map (parsePositionRow position)

let parseAll dataPath = List.map (parsePositionFile dataPath) positions |> Seq.concat |> List.ofSeq
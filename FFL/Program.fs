open Parsing
open Scoring
open Analysis
open FileIO
open Leagues

let dataPath = @"..\..\..\data\2015\"
let columns = "Player,Position,Score,Marginal Value"

let analyze league = 
    let players = parseAll dataPath |> List.map (fun player-> (player, Scoring.scorePlayer league.Scoring player))
    let scored = Analysis.calculateMarginalValue league.Roster players

    let output = scored |> Seq.map (fun (player, score, marginalValue)-> sprintf "%s,%s,%0.2f,%0.2f" player.Name player.Position score marginalValue) |> String.concat "\n"
    FileIO.writeFile (dataPath+league.Name+"_FFL.csv") (columns+"\n"+output)

analyze auctionLeague
analyze carolinaLeague
open Parsing
open Scoring
open Analysis
open FileIO
open Leagues

let year = string System.DateTime.Now.Year
let dataPath = @"..\..\..\data\"+year+"\\"
let columns = "Player,Position,Score,Value over Avg,Value over Next"

let analyze league = 
    let players = parseAll dataPath |> List.map (fun player-> (player, Scoring.scorePlayer league.Scoring player))
    let scored = Analysis.calculateMarginalValue league.Roster players

    let output =
        scored 
            |> Seq.map (fun player -> sprintf "%s,%s,%0.2f,%0.2f,%0.2f" player.Player.Name player.Player.Position player.Score player.ScoreOverBaseline player.ScoreOverNext)
            |> String.concat "\n"
    FileIO.writeFile (dataPath+league.Name+"_FFL_"+year+".csv") (columns+"\n"+output)

analyze auctionLeague
analyze carolinaLeague
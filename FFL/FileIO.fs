module FileIO
open System.IO

let loadFile filePath =
    using (new StreamReader(File.OpenRead filePath)) (fun rdr->
        rdr.ReadToEnd())

let writeFile filePath (content:string) =
    using (new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Write))) (fun wr->
        wr.Write content)
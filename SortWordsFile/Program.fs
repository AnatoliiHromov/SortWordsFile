// Learn more about F# at http://fsharp.org

open System
open SortLargeFile

[<EntryPoint>]
let main argv =
    let c = SortLargeFile("Text.txt", 10000, "res.txt")
    printfn "Hello World from F#!"
    printfn "%A" c
    0 // return an integer exit code

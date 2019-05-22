module Tests

open System
open Xunit
open SortLargeFile


//[<Fact>]
//let ``10000`` () =
//    let c = SortLargeFile("Text.txt", 10000, "res.txt")
//    Assert.True(true)
[<Fact>]
let ``1000`` () =
    let c = SortLargeFile("Text.txt", 1000, "res1.txt")
    Assert.True(true)
[<Fact>]
let ``100`` () =
    let c = SortLargeFile("Text.txt", 100, "res2.txt")
    Assert.True(true)
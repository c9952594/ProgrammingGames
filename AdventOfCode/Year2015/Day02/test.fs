module Year2015Day02Test

open System
open NUnit.Framework
open Year2015Day02
open Common

[<Test>]
let Year2015Day02 () =
    let input = 
        "./Year2015/Day02/input"
        |> (parseInput << fileInput)
        |> List.ofSeq
        |> List.last
    printfn "%A" input
    Assert.Fail("Failed to show output")

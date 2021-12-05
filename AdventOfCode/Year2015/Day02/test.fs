module Year2015Day02Test

open System
open NUnit.Framework
open Year2015Day02
open Common

open FParsec

[<Test>]
let Year2015Day02 () =
    let input = 
        "./Year2015/Day02/input"
        |> (parseInput << fileInput)

    let output = part1 input

    printfn "%A" output

    // Assert.AreEqual( 74   , part1 input )

    Assert.Fail("Failed to show output")
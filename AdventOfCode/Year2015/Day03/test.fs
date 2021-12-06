module Year2015Day03Test

open System
open NUnit.Framework
open Year2015Day03
open Common

open FParsec

[<Test>]
let Year2015Day02 () =
    let input = 
        "./Year2015/Day03/input"
        |> (parseInput << combineIntoOneString << fileInputReadAllLines)
    
    let output = part2 input
    printfn "Part 2:\n%A" output

    Assert.AreEqual( 2565 , part1 input )
    Assert.AreEqual( 2639 , part2 input )

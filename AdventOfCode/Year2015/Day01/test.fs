module Year2015Day01Test

open System
open NUnit.Framework
open Year2015Day01
open Common

[<Test>]
let Year2015Day01 () =
    let input = 
        "./Year2015/Day01/input"
        |> (parseInput << fileInput) 
    Assert.AreEqual( 74   , part1 input )
    Assert.AreEqual( 1795 , part2 input )
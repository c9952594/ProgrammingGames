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
        |> (parseInput << combineIntoOneString << fileInputReadAllLines)
    
    Assert.AreEqual( 1586300 , part1 input )
    Assert.AreEqual( 3737498 , part2 input )
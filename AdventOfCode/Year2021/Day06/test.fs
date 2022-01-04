module Year2021Day06Test

open System
open NUnit.Framework
open Year2021Day06
open Common

[<Test>]
let Year2021Day06 () =
    let report = 
        "./Year2021/Day06/input"
        //"./Year2021/Day06/example"
        |> (fileInputReadAllLines >> combineIntoOneString >> parseInput)

    Assert.AreEqual( 358214uL    , part1 report )
    Assert.AreEqual( 1622533344325uL    , part2 report )
    
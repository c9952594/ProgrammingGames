module Year2021Day05Test

open System
open NUnit.Framework
open Year2021Day05
open Common

[<Test>]
let Year2021Day05 () =
    let report = 
        "./Year2021/Day05/input"
        //"./Year2021/Day05/example"
        |> (fileInputReadAllLines >> combineIntoOneString >> parseInput)

    Assert.AreEqual( 6397    , part1 report )
    Assert.AreEqual( 22335    , part2 report )
    
module Year2021Day07Test

open System
open NUnit.Framework
open Year2021Day07
open Common

[<Test>]
let Year2021Day07 () =
    let report = 
        "./Year2021/Day07/input"
        //"./Year2021/Day07/example"
        |> (fileInputReadAllLines >> combineIntoOneString >> parseInput)

    //printf "%A" (part1 report)
    //printf "%A" (part2 report)
    //Assert.Fail()

    Assert.AreEqual( 337488    , part1 report )
    Assert.AreEqual( 89647695    , part2 report )
    
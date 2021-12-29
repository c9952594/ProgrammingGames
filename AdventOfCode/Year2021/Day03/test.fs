module Year2021Day03Test

open System
open NUnit.Framework
open Year2021Day03
open Common

[<Test>]
let Year2021Day03 () =
    let report = 
        "./Year2021/Day03/input"
        |> (fileInputReadAllLines >> parseInput)

    Assert.AreEqual( 2967914    , part1 report )
    Assert.AreEqual( 7041258    , part2 report )      
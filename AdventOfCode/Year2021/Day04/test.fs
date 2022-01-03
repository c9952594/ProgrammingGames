module Year2021Day04Test

open System
open NUnit.Framework
open Year2021Day04
open Common

[<Test>]
let Year2021Day04 () =
    let report = 
        "./Year2021/Day04/input"
        //"./Year2021/Day04/example"
        |> (fileInputReadAllLines >> combineIntoOneString >> parseInput)

 
    Assert.AreEqual( 6592u    , part1 report )
    Assert.AreEqual( 31755u    , part2 report )
    
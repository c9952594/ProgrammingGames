module Year2021Day01Test

open System
open NUnit.Framework
open Year2021Day01
open Common

[<Test>]
let Year2021Day01 () =
    let input = 
        "./Year2021/Day01/input"
        |> (fileInputReadAllLines >> parseInput)
  
    Assert.AreEqual (1298, part1 input)
    Assert.AreEqual (1248, part2 input)

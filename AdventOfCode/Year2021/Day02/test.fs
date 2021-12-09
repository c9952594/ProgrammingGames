module Year2021Day02Test

open System
open NUnit.Framework
open Year2021Day02
open Common

[<Test>]
let Year2021Day02 () =
    let input = 
        "./Year2021/Day02/input"
        |> (fileInputReadAllLines >> combineIntoOneString >> parseInput)
  
    match input with
    | Ok commands ->
        Assert.AreEqual( 1648020    , part1 commands )
        Assert.AreEqual( 1759818555 , part2 commands )
    | Error error ->
        Assert.Fail (sprintf "%s" error)
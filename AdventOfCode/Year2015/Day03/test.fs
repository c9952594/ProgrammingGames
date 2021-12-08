module Year2015Day03Test

open System
open NUnit.Framework
open Year2015Day03
open Common

[<Test>]
let Year2015Day03 () =
    let input = 
        "./Year2015/Day03/input"
        |> (fileInputReadAllLines >> combineIntoOneString >> parseInput)
  
    match input with
    | Ok directions ->
        Assert.AreEqual( 2565 , part1 directions )
        Assert.AreEqual( 2639 , part2 directions )
    | Error error ->
        failwithf "%s" error

module Year2015Day04Test

open System
open NUnit.Framework
open Year2015Day04
open Common
open System.Text

[<Test>]
let Year2015Day04 () =
    let secretKey  = "iwrupvqb"

    Assert.AreEqual( 346386 ,  part1 secretKey )
    Assert.AreEqual( 9958218 , part2 secretKey )
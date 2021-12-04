module AdventOfCodeTest

open System
open NUnit.Framework
open AdventOfCode.Year2015Day1

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    Assert.AreEqual("ASD", capitalise("asd"))

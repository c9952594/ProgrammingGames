module Common

open System

type LinesOfInput = LinesOfInput of string seq

let consoleInput () :LinesOfInput =
    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (not << isNull)
    |> LinesOfInput

let fileInput (filepath:string) :LinesOfInput =
    System.IO.File.ReadAllLines(filepath)
    |> Seq.ofArray
    |> LinesOfInput

let stringInput (input: string) :LinesOfInput =
    let reader = new System.IO.StringReader(input)
    Seq.initInfinite (fun _ -> reader.ReadLine())
    |> Seq.takeWhile (not << isNull)
    |> LinesOfInput
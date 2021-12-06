module Common

open System

let consoleInput () :string seq =
    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (not << isNull)

let fileInputReadAllLines (filepath:string) :string seq =
    System.IO.File.ReadAllLines(filepath)
    |> Seq.ofArray

let stringInput (input: string) :string seq =
    let reader = new System.IO.StringReader(input)
    Seq.initInfinite (fun _ -> reader.ReadLine())
    |> Seq.takeWhile (not << isNull)

let combineIntoOneString (input: string seq) :string = 
    String.concat "\n" input
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

let flip f x y = 
    f y x

type Bit =
    | True
    | False

let binaryToInt (bits: Bit seq) :int =
    Seq.foldBack (fun bit (total, unitValue) -> 
        let newTotal =
            match bit with
            | True  -> total + unitValue
            | False -> total
        let nextUnitValue = unitValue * 2
        (newTotal , nextUnitValue)
    ) bits (0, 1)
    |> (fun (total, unitValue) -> total)
// https://adventofcode.com/2015/day/2

open System

let (|Digit|_|) (input:char) = 
    match input with
    | '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' -> Some(input)
    | _ -> None

let consoleInput =
    Seq.initInfinite (fun _ -> Console.ReadLine())
    |> Seq.takeWhile (not << isNull)
    |> Seq.map (
        function 
        | '(' -> OK UpALevel
        | ')' -> OK DownALevel
        | badChar -> Error badChar
    )
    |> Seq.map (fun instruction ->
        match instruction with
        | OK instruction -> instruction
        | Error _ -> failwith "Bad char"
    )
    
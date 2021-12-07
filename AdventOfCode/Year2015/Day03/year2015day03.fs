module Year2015Day03

open Common
open FParsec

type Direction =
    | Left 
    | Right
    | Up 
    | Down 

let parseInput (input:string) =
    let directions =
        many (
            (pchar '<' >>% Left)  <|>
            (pchar '>' >>% Right) <|>
            (pchar '^' >>% Up)    <|>
            (pchar 'v' >>% Down)
        )

    run directions input
    |> (
        function
        | Success(result, _, _) -> 
            Result.Ok result
        | Failure(errorMsg, _, _) -> 
            Result.Error errorMsg
    )

type X = X of int
type Y = Y of int
type Position = Position of X * Y

let positions (directions: Direction seq) :Position seq =
    let startPosition = Position (X 0, Y 0)

    directions
    |> Seq.scan (fun (Position (X x, Y y)) direction ->
        Position (
            X (    
                match direction with
                | Left -> x - 1
                | Right -> x + 1
                | _ -> x
            ),
            Y (
                match direction with
                | Up -> y + 1
                | Down -> y - 1
                | _ -> y
            )
        )
    ) startPosition

let part1 (directions: Direction seq) = 
    positions directions
    |> Seq.distinct
    |> Seq.length

let part2 (directions: Direction seq) =
    directions
    |> Seq.chunkBySize 2
    |> (Seq.foldBack >> flip)
        (fun chunk (santaDirections, robotsDirections) -> 
            chunk.[0]::santaDirections, chunk.[1]::robotsDirections
        ) ([],[])
    |> fun (santasDirections, robotsDirections)->
        Seq.append 
            (positions (Seq.ofList santasDirections))
            (positions (Seq.ofList robotsDirections))
        |> Seq.distinct
        |> Seq.length
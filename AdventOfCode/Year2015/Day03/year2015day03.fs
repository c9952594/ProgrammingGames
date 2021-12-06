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
        |>> Seq.ofList

    run directions input
    |> (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

type X = X of int
type Y = Y of int
type Position = Position of X * Y
let StartPosition = Position (X 0, Y 0)

let positions (directions: Direction seq) :Position seq =
    directions
    |> Seq.scan (fun (Position (X x, Y y)) direction ->
        Position (
            (X (    
                match direction with
                | Left -> x - 1
                | Right -> x + 1
                | _ -> x
            ),
            (Y (
                match direction with
                | Up -> y + 1
                | Down -> y - 1
                | _ -> y
            )))
        )
    ) StartPosition

let part1 (directions: Direction seq) = 
    positions directions
    |> Seq.distinct
    |> Seq.length

let part2 (directions: Direction seq) =
    let swap f x y = f y x

    directions
    |> Seq.chunkBySize 2
    |> (swap << Seq.foldBack)
        (fun chunk (santas, robots) -> 
            chunk.[0]::santas, chunk.[1]::robots
        ) ([],[])
    |> fun (santas, robots)->
        positions (Seq.ofList santas)
        |> Seq.append (positions (Seq.ofList robots))
        |> Seq.distinct
        |> Seq.length
        
    
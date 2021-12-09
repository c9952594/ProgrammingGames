module Year2021Day02

open Common
open FParsec

type Command =
    | Forward of int
    | Down of int
    | Up of int

let parseInput (input:string) =
    let command instruction = 
        (pstring instruction) >>. spaces >>. pint32 .>> spaces

    let commands =
        spaces >>. many (
            (command "forward" |>> Forward) <|> 
            (command "down" |>> Down) <|> 
            (command "up" |>> Up)
        )
        
    (run commands input)
    |> (
        function
        | Success (result, _, _) ->
            Result.Ok result
        | Failure (errorMsg, _, _) -> 
            Result.Error errorMsg
    )

let part1 (commands: Command list) =     
    commands
    |> List.fold (fun (horizontal, depth) command -> 
        match command with
            | Forward x -> ( horizontal + x , depth     )
            | Down x    -> ( horizontal     , depth + x )
            | Up x      -> ( horizontal     , depth - x )
        ) (0, 0)
    |> (fun (horizontal, depth) -> horizontal * depth)

let part2 (commands: Command list) = 
    commands
    |> List.fold (fun (horizontal, depth, aim) command -> 
        match command with
            | Forward x -> ( horizontal + x , depth + (aim * x) , aim     )
            | Down x    -> ( horizontal     , depth             , aim + x )
            | Up x      -> ( horizontal     , depth             , aim - x )
        ) (0, 0, 0)
    |> (fun (horizontal, depth, _)  -> horizontal * depth)
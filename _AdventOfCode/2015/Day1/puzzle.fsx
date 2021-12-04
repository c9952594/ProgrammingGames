// https://adventofcode.com/2015/day/1

open System

type Instruction =
    | UpALevel
    | DownALevel

let consoleInput =
    Console.ReadLine()
    |> Seq.map (function 
        | '(' -> UpALevel
        | ')' -> DownALevel
        | _ -> failwith "Bad char"
    )
    
let part1 (instructions:Instruction seq) :int = 
    instructions
    |> Seq.sumBy (function
        | UpALevel -> 1
        | DownALevel -> -1
    )

let part2 (instructions:Instruction seq) :int = 
    instructions
    |> Seq.scan (fun (currentFloor, totalFloors) instruction -> 
        match instruction with
        | UpALevel   -> (currentFloor + 1 , totalFloors + 1)
        | DownALevel -> (currentFloor - 1 , totalFloors + 1)
    ) (0, 0)
    |> Seq.find (fun (currentFloor, _) -> currentFloor = -1)
    |> snd

printfn "part1   74: %d" (part1 consoleInput)
printfn "part2 1795: %d" (part2 consoleInput)
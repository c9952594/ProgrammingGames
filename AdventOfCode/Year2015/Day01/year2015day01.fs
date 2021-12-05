module Year2015Day01

open Common

type Instruction =
    | UpALevel
    | DownALevel
type Instructions = Instructions of Instruction seq

let parseInput (input:LinesOfInput) :Instructions =
    input
    |> Seq.head
    |> Seq.map (function 
        | '(' -> UpALevel
        | ')' -> DownALevel
        | _ -> failwith "Bad char"
    )
    |> Instructions

let part1 (Instructions instructions) :int = 
    instructions
    |> Seq.sumBy (function
        | UpALevel -> 1
        | DownALevel -> -1
    )

type private CurrentFloor = CurrentFloor of int
type private FloorChanges = FloorChanges of int
type private Position = Position of CurrentFloor * FloorChanges
let private StartPosition = Position (CurrentFloor 0, FloorChanges 0)

let part2 (Instructions instructions) =
    instructions
    |> Seq.scan (
        fun (Position (CurrentFloor currentFloor, FloorChanges floorChanges)) instruction ->
            Position (
                CurrentFloor (
                    match instruction with
                        | UpALevel -> (+)
                        | DownALevel -> (-)
                    |> fun nextFloor -> 
                        nextFloor currentFloor 1
                ),
                FloorChanges (floorChanges + 1)
            )
    ) StartPosition
    |> Seq.find (fun (Position (CurrentFloor currentFloor, _)) -> currentFloor = -1)
    |> (fun (Position (_, FloorChanges floorChanges)) -> floorChanges)
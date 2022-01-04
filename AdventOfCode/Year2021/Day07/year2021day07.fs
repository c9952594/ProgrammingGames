module Year2021Day07

open Common
open FParsec

let parseInput (input: string) = 
    let horizontalPosition = 
        pint32 .>> (optional (skipChar ','))
    let crabs =
        (many horizontalPosition)
        
    run crabs input
    |> (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

let part1 (crabPositions : int list) = 
    let furthest =
        crabPositions
        |> List.maxBy id

    [0..furthest]
    |> List.map (fun checkPosition ->
        crabPositions
        |> List.sumBy (fun position -> 
            let distanceToCheckPosition = 
                abs (checkPosition - position)
            distanceToCheckPosition
        )
    )
    |> List.sortBy id
    |> List.head
    
let part2 crabPositions = 
    let furthest =
        crabPositions
        |> List.maxBy id

    [0..furthest]
    |> List.map (fun checkPosition ->
        crabPositions
        |> List.sumBy (fun position -> 
            let distanceToCheckPosition = 
                (abs (checkPosition - position))
            let fuelRequired = 
                ((distanceToCheckPosition) * (distanceToCheckPosition + 1)) / 2
            fuelRequired    
        )
    )
    |> List.sortBy id
    |> List.head
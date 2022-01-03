module Year2021Day05

open Common
open FParsec

type Coordinate =
    | Coordinate of uint32 * uint32

type Line = 
    | Line of Coordinate * Coordinate

type Points =
    | Points of Coordinate list

    static member ofLine (Line (fromPoint, toPoint) ) = 

        let rec coordinates ( currentPoint ) = seq {
            yield currentPoint

            if (currentPoint <> toPoint)
            then
                let ( Coordinate (x1, y1), Coordinate (x2, y2) ) = ( currentPoint, toPoint )

                let nextPoint =
                    Coordinate (
                        if (x1 = x2)
                        then x1
                        else
                            if (x1 < x2)
                            then x1 + 1u
                            else x1 - 1u
                        ,
                        if (y1 = y2)
                        then y1
                        else
                            if (y1 < y2)
                            then y1 + 1u
                            else y1 - 1u
                    )

                yield! coordinates nextPoint
        }

        Points (
            coordinates fromPoint
            |> List.ofSeq
        )

type Lines =
    | Lines of Line list

let parseInput (input: string) = 
    let coordinate = 
        puint32 .>>. (skipChar ',' >>. puint32)
        |>> Coordinate
    let divider =
        skipString " -> "
    let line =
        coordinate .>>. (divider >>. coordinate)
        |>> Line
    let lines =
        many (line .>> spaces)
        |>> Lines

    run lines input
    |> (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

let part1 (Lines input) = 
    input
    |> List.filter (fun (Line ( Coordinate (x1, y1), Coordinate (x2, y2))) ->
        x1 = x2 || y1 = y2
    )
    |> List.map Points.ofLine    
    |> List.map (fun (Points a) -> a)
    |> List.reduce List.append
    |> List.countBy id
    |> List.filter (fun (_, count) -> count >= 2)
    |> List.length

let part2 (Lines input) = 
    input
    |> List.map Points.ofLine
    |> List.map (fun (Points a) -> a)
    |> List.reduce List.append
    |> List.countBy id
    |> List.filter (fun (_, count) -> count >= 2)
    |> List.length
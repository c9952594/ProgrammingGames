module Year2021Day06

open Common
open FParsec

let parseInput (input: string) = 
    let age = 
        puint32 .>> (optional (skipChar ','))
    let ages =
        (many age)
        |>> (fun ages ->
            ages
            |> List.groupBy id
            |> List.map (fun (age, groupedByAge) -> 
                let countOfFishAtThatAge = (uint64 << List.length) groupedByAge
                (age, countOfFishAtThatAge)
            )
            |> Map.ofList
        )

    run ages input
    |> (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

let rec seqOfDays (currentAges : Map<uint32, uint64>) = seq {
    yield currentAges

    let nextAges =
        currentAges
        |> Map.fold (fun newAges currentAge countOfAge -> 
            let new6 = 
                newAges
                |> Map.tryFind 6u
                |> (function
                    | None   -> countOfAge
                    | Some x -> countOfAge + x
                )

            match currentAge with
            | 0u ->
                newAges 
                |> Map.add 6u new6
                |> Map.add 8u countOfAge
            | 7u ->
                newAges 
                |> Map.add 6u new6
            | _ ->
                newAges
                |> Map.add (currentAge - 1u) countOfAge    
        ) Map.empty

    yield! seqOfDays nextAges
}

let part1 (input : Map<uint32,uint64>) = 
    input
    |> seqOfDays
    |> Seq.skip 1
    |> Seq.take 80
    |> Seq.last
    |> Map.toList
    |> List.sumBy (fun (_, value) -> 
        value
    )

let part2 (input : Map<uint32,uint64>) = 
    input
    |> seqOfDays
    |> Seq.skip 1
    |> Seq.take 256
    |> Seq.last
    |> Map.toList
    |> List.sumBy (fun (_, value) -> 
        value
    )
    
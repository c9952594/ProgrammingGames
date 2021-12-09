module Year2021Day01

open Common
open FParsec

let parseInput (input:string seq) =
    input 
    |> Seq.map int

let part1 (input: int seq) = 
    input
    |> Seq.windowed 2
    |> Seq.sumBy (fun a -> 
        if a.[1] > a.[0] 
            then 1
            else 0
    )

let part2 (input: int seq) = 
    input
    |> Seq.windowed 3
    |> Seq.map Seq.sum
    |> part1
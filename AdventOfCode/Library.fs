namespace AdventOfCode

open System

module Say =
    let hello name =
        printfn "Hello %s" name

    let capitalise (input: string) = input.ToUpper()
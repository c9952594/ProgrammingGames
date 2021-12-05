module Year2015Day02

open Common
open FParsec

type Length = Length of uint64
type Width  = Width of uint64
type Height = Height of uint64
type Package = Package of Length * Width * Height

let parseInput (input:LinesOfInput) :Package seq =
    let parser =
        let dimension = puint64 .>> optional (skipChar 'x')
        tuple3
            (dimension |>> Length)
            (dimension |>> Width) 
            (dimension |>> Height)
        |>> Package
        
    input
    |> Seq.map (run parser)
    |> Seq.map (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

let part1 (packages: Package seq) = 
    packages
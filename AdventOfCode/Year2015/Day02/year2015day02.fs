module Year2015Day02

open Common
open FParsec

type Length = Length of uint64
type Width  = Width of uint64
type Height = Height of uint64
type Package = 
    | Package of Length * Width * Height
        
let parseInput (input:string) :Package seq =
    let dimension = 
        puint64 .>> optional (skipChar 'x')

    let packageDimensions =
        tuple3
            (dimension |>> Length)
            (dimension |>> Width) 
            (dimension |>> Height)
        .>> spaces

    let package =
        packageDimensions
        |>> Package

    let packages =
        many package
        |>> Seq.ofList
    
    run packages input
    |> (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

let surfaceArea (Package (Length l, Width w, Height h)) =
    2UL * (l * w) +
    2UL * (w * h) +
    2UL * (h * l)
    
let areaOfSmallestFace (Package (Length l, Width w, Height h)) =
    Array.min [|
        l * w
        l * h
        w * h
    |]

let cubicFeetOfVolume (Package (Length l, Width w, Height h)) =
    l * w * h

let shortestDistanceAroundSides (Package (Length l, Width w, Height h)) =
    Array.min [|
        2UL * (l + w)
        2UL * (l + h)
        2UL * (w + h)
    |]

let part1 (packages: Package seq) = 
    packages
    |> Seq.map (fun package -> 
        surfaceArea package +
        areaOfSmallestFace package
    )
    |> Seq.sum

let part2 (packages: Package seq) = 
    packages
    |> Seq.map (fun package -> 
        shortestDistanceAroundSides package +
        cubicFeetOfVolume package
    )
    |> Seq.sum
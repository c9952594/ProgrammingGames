module Year2021Day03

open Common

let parseInput (input:string seq) =
    input
    |> (Seq.map >>Seq.map) (function
        | '0' -> False
        | '1' -> True
        | errorChar -> failwith (sprintf "%c: Not 0 or 1" errorChar)
    )

let countOfTrueAndFalsePerColumn (report: Bit seq seq) =
    report
    |> (Seq.map >> Seq.map) (
        function
        | True ->  ( (+) 1 , (+) 0 )
        | False -> ( (+) 0 , (+) 1 ) 
    )
    |> Seq.reduce (fun first second -> 
        first
        |> Seq.zip  second
        |> Seq.map (
            fun ( (firstTrue, firstFalse), (secondTrue, secondFalse) ) -> 
                (firstTrue >> secondTrue , firstFalse >> secondFalse)
        )
    )
    |> Seq.map (fun (countTrue, countFalse) -> 
        countTrue 0, countFalse 0
    )

let part1 (report: Bit seq seq) =    
    report
    |> countOfTrueAndFalsePerColumn
    |> Seq.fold (fun (gamma, epsilon) (trueCount, falseCount) ->
        if trueCount > falseCount
        then  
            True::gamma  , False::epsilon
        else 
            False::gamma , True::epsilon
    ) ([], [])
    |> fun (gammaBits, epsilonBits) -> 
        ( Seq.rev gammaBits , Seq.rev epsilonBits )
    |> fun (gammaBits, epsilonBits) ->
        binaryToInt gammaBits , binaryToInt epsilonBits
    |> fun (gamma, epsilon) -> 
        gamma * epsilon

type TreeNode = 
    | TreeNode of TreeNode * TreeNode
    | Leaf of int * Bit seq * TreeNode
    | NoLeaf

let createTree (lines: Bit seq seq) : TreeNode = 
    let rec _createTree (path : Bit list) (_lines: Bit seq seq) : TreeNode = 

        let (trues, falses) = 
            _lines
            |> Seq.fold (fun (ts, fs) line -> 
                if (Seq.isEmpty line) 
                then
                    ( ts, fs )
                else
                    let head = Seq.head line
                    let tail = Seq.tail line
                    
                    match head with
                    | True  -> ( tail::ts ,       fs )
                    | False -> (       ts , tail::fs )
            ) ([], [])

        let countOfTrues = List.length trues
        let trueNode = 
            match countOfTrues with
            | 0 -> 
                NoLeaf
            | _ -> 
                let withTrue = path @ [True]
                let next = _createTree withTrue trues
                Leaf (countOfTrues, withTrue, next)

        let countOfFalses = List.length falses
        let falseNode = 
            match countOfFalses with
            | 0 -> 
                NoLeaf
            | _ -> 
                let withFalse = path @ [False]
                let next = _createTree withFalse falses
                Leaf (countOfFalses, withFalse, next)

        TreeNode (trueNode, falseNode)

    _createTree [] lines


let rec oxygenGeneratorRating = 
    function
    | TreeNode (Leaf (1, bits, (TreeNode (NoLeaf, NoLeaf))), _)
    | TreeNode (_, Leaf (1, bits, (TreeNode (NoLeaf, NoLeaf)))) ->
        bits
    | TreeNode (Leaf (trueCount, _, trueTreeNode), Leaf (falseCount, _, falseTreeNode)) -> 
        if (trueCount >= falseCount)
        then oxygenGeneratorRating trueTreeNode
        else oxygenGeneratorRating falseTreeNode
    | TreeNode (Leaf (_, _, treeNode), _)
    | TreeNode (_, Leaf (_, _, treeNode)) -> 
        oxygenGeneratorRating treeNode
    | uncaught -> failwith (sprintf "Uncaught: %A" uncaught)

let rec co2ScrubberRating = 
    function
    | TreeNode (Leaf (1, bits, (TreeNode (NoLeaf, NoLeaf))), _)
    | TreeNode (_, Leaf (1, bits, (TreeNode (NoLeaf, NoLeaf)))) ->
        bits
    | TreeNode (Leaf (trueCount, _, trueTreeNode), Leaf (falseCount, _, falseTreeNode)) -> 
        if (trueCount >= falseCount)
        then co2ScrubberRating falseTreeNode
        else co2ScrubberRating trueTreeNode
    | TreeNode (Leaf (_, _, treeNode), _)
    | TreeNode (_, Leaf (_, _, treeNode)) -> 
        co2ScrubberRating treeNode
    | uncaught -> failwith (sprintf "Uncaught: %A" uncaught)

let part2 (report: Bit seq seq)  =
    let cachedReport = Seq.cache report

    let bitTree = createTree cachedReport

    let o2 = 
        bitTree
        |> oxygenGeneratorRating  
        |> binaryToInt

    let co2 = 
        bitTree
        |> co2ScrubberRating  
        |> binaryToInt
    
    o2 * co2
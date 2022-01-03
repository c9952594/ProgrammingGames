module Year2021Day04

open Common
open FParsec

type BingoBoardNumber =
    | Unmarked of uint32
    | Marked of uint32

    static member mark (number : uint32) (bingoBoardNumber : BingoBoardNumber) : BingoBoardNumber =
        match bingoBoardNumber with
        | Unmarked unwrapped when unwrapped = number ->
            Marked unwrapped
        | _ -> 
            bingoBoardNumber

type WinningNumber =
    | WinningNumber of uint32
type WinningLine = 
    | WinningLine of uint32 list    
type BingoGrid = 
    | BingoGrid of BingoBoardNumber list list

    static member mark (number : uint32) (BingoGrid numbers) =
        numbers
        |> (List.map >> List.map) 
            (BingoBoardNumber.mark number)
        |> BingoGrid

type MarkedNumbers = 
    | MarkedNumbers of uint32 list    
type BingoBoard = 
    | Unwon of MarkedNumbers * BingoGrid
    | Won of MarkedNumbers * WinningNumber * WinningLine * BingoGrid

    static member mark (number : uint32) (board : BingoBoard) =
        match board with
        | Unwon (MarkedNumbers markedNumbers, grid) ->
            let nextGrid = BingoGrid.mark number grid
            let nextMarkedNumbers = markedNumbers @ [number]

            let winCheckGrid = 
                nextGrid
                |> (fun (BingoGrid numbers) -> numbers)
                |> (List.map >> List.map) (fun num -> 
                    match num with
                    | Unmarked x -> 
                        ( (&&) false , x )
                    | Marked x   -> 
                        ( (&&) true  , x )
                )

            let findWinningLine line =
                let (isWinning, winningLine) =
                    line 
                    |> List.fold 
                        (fun (currentlyWinning, acc) ( checkFunc, num ) ->
                            ( checkFunc currentlyWinning , num::acc )
                        ) (true, [])

                match isWinning with
                | false -> None
                | true -> Some (winningLine |> List.rev)

            let winningRow =
                winCheckGrid
                |> List.tryPick findWinningLine

            let winningColumn =
                winCheckGrid
                |> List.transpose
                |> List.tryPick findWinningLine

            match (winningRow, winningColumn) with
            | None             , None -> 
                Unwon (MarkedNumbers nextMarkedNumbers, nextGrid)
            | None             , Some winningLine
            | Some winningLine , None
            | Some winningLine , Some _ ->
                Won (
                    MarkedNumbers nextMarkedNumbers, 
                    WinningNumber number,
                    WinningLine winningLine,
                    nextGrid
                )
            
        | _ ->
            board
            
type RemainingBingoNumbers = 
    | RemainingBingoNumbers of uint32 list
type BingoGame =
    | BingoGame of RemainingBingoNumbers * BingoBoard list

    static member turns game = seq {
        yield game

        let (BingoGame (RemainingBingoNumbers remaining, _)) = game

        if ((not << List.isEmpty) remaining)
        then yield! BingoGame.turns (BingoGame.mark game)
    }

    static member mark (BingoGame (RemainingBingoNumbers remaining, boards)) =
        match remaining with
        | [] -> 
            BingoGame (RemainingBingoNumbers remaining, boards)
        | _ ->
            let markBoard = BingoBoard.mark  ( List.head remaining )
            
            BingoGame (
                RemainingBingoNumbers (  
                    List.tail remaining 
                )
                , 
                boards
                |> List.map markBoard 
            )

let parseInput (input: string) = 
    let remainingBingoNumbers = 
        let pickedNumber = 
            (puint32 .>> optional (skipChar ','))

        ((many pickedNumber) .>> spaces) 
        |>> RemainingBingoNumbers

    let bingoCard =
        let bingoNumber = 
            (optional spaces >>. puint32)
            |>> Unmarked

        let bingoLine =
            bingoNumber .>>. bingoNumber .>>. bingoNumber .>>. bingoNumber .>>. bingoNumber 
            |>> (fun ((((a,b),c),d),e) ->
                [a;b;c;d;e]
            )

        bingoLine .>>. bingoLine .>>. bingoLine .>>. bingoLine .>>. bingoLine
        |>> (fun ((((a,b),c),d),e) ->
            Unwon (
                MarkedNumbers List.empty,
                [a;b;c;d;e]
                |> BingoGrid
            )
        )

    let bingoCards =
        many bingoCard

    let bingoGame = 
        (remainingBingoNumbers .>>. bingoCards)
        |>> (fun (unpickedNumbers, bingoCards) ->
             BingoGame (unpickedNumbers, bingoCards)
        )

    run bingoGame input
    |> (
        function
        | Success(result, _, _) -> 
            result
        | Failure(errorMsg, _, _) -> 
            failwithf "Bad Parse:\n%s" errorMsg
    )

let part1 (input : BingoGame) = 
    (BingoGame.turns input)
    |> Seq.tryPick (fun (BingoGame (_, boards)) ->
        boards
        |> List.tryPick (
            function
            | Unwon _ -> 
                None
            | Won (_, WinningNumber winningNumber, _, (BingoGrid lines)) ->
                let sumOfUnpicked = 
                    lines
                    |> (List.sumBy >> List.sumBy) (   
                        function
                        | Unmarked y -> y
                        | Marked   _ -> 0u
                    )
               
                Some (winningNumber * sumOfUnpicked)
        )
    )
    |> (
        function
        | None   -> failwith "No win"
        | Some x -> x
    )


let part2 (input: BingoGame) = 
    (BingoGame.turns input)
    |> Seq.last
    |> (fun (BingoGame (_, boards)) ->
        boards
    )
    |> Seq.sortByDescending (fun board ->
        match board with
        | Unwon _ -> 
            0
        | Won (MarkedNumbers n, _, _, _) ->
            List.length n
    )
    |> Seq.head
    |> (fun (board : BingoBoard) ->
        match board with
        | Unwon _ -> 
            failwith "No winning solution"
        | Won (_, WinningNumber winningNumber, _, BingoGrid lines) ->
            let sumOfUnpicked = 
                lines
                |> (List.sumBy >> List.sumBy) (   
                    function
                    | Unmarked y -> y
                    | Marked   _ -> 0u
                )
            
            winningNumber * sumOfUnpicked
    )
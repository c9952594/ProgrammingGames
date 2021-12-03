//open System

// let consoleInput =
//     Seq.initInfinite (fun _ -> Console.ReadLine())
//     |> Seq.takeWhile (not << isNull)
//     |> Seq.head
//     |> Seq.map (
//         function 
//         | '(' -> OK UpALevel
//         | ')' -> OK DownALevel
//         | badChar -> Error badChar
//     )
//     |> Seq.map (fun instruction ->
//         match instruction with
//         | OK instruction -> instruction
//         | Error _ -> failwith "Bad char"
//     )
    






// type Fruit = Apple | Pear | Orange

//  type BagItem = { fruit: Fruit; quantity: int }

//  let takeMore (previous: BagItem list) fruit = 
//      let toTakeThisTime = 
//          match previous with 
//          | bagItem :: otherBagItems -> bagItem.quantity + 1 
//          | [] -> 1 
//      { fruit = fruit; quantity = toTakeThisTime } :: previous

//  let inputs = [ Apple; Pear; Orange ]
 
//  ([], inputs) ||> List.fold takeMore
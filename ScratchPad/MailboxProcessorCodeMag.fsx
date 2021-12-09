open System

// let myFirstAgent =
//     MailboxProcessor<string>.Start (fun inbox -> 
//         async { 
//             while true do
//                 let! msg = inbox.Receive()
//                 printfn "got message '%s'" msg
//         }
//     )

// myFirstAgent.Post "Hello!"

// type Message = string * AsyncReplyChannel<string>

// let replyAgent =
//     MailboxProcessor<Message>.Start(fun inbox ->
//         let rec loop () =
//             async {
//                 let! (message, replyChannel) = inbox.Receive()
//                 replyChannel.Reply 
//                     (sprintf "Received message: %s" message)
//                 do! loop ()
//             }
//         loop ()
//     )

// let reply = replyAgent.PostAndReply(fun rc -> "Hello", rc)
// printfn "%A" reply

// let myFirstAgent =
//     MailboxProcessor.Start(fun inbox ->
//         async { 
//             while true do
//                 do! inbox.Scan (fun hello ->
//                     match hello with
//                     | "Hello!" ->
//                         Some(
//                             async {
//                                 printfn "This is a hello message!"
//                             }
//                         )
//                     | _ -> None
//                 )

//                 let! msg = inbox.Receive()
//                 printfn "Got message '%s'" msg
//         }
//     )

// ["1"; "2"; "3"; "4"; "5"; "6"; "7"; "8"; "9"; "10";
//  "Hello!"; "Hello!"; "Hello!"; "Hello!"; "Hello!"; "Hello!"; 
//  "Hello!"; "Hello!"; "Hello!"; "Hello!"]
// |> List.map myFirstAgent.Post

type CoordinatorMessage =
    | Job of int
    | Ready
    | RequestJob of AsyncReplyChannel<int>

let Coordinator =
    MailboxProcessor<CoordinatorMessage>.Start(fun inbox ->
        let queue = new System.Collections.Generic.Queue<int>()
        let mutable count = 0

        let rec loop () = 
            async {
                while count < 4 do
                    do! inbox.Scan (function
                        | Ready -> 
                            printfn "Ready"
                            Some(async {
                                printfn "Count: %i" count
                                count<-count+1
                            })
                        | Job l -> None
                        | RequestJob r -> None
                    )

                let! message = inbox.Receive()
                match message with
                | Job length -> 
                    queue.Enqueue length
                | RequestJob replyChannel ->
                    replyChannel.Reply <| queue.Dequeue()
                | Ready -> 
                    ()

                return! loop ()
            }
        loop ()
    )

let Worker () =
    MailboxProcessor<bool>.Start (fun inbox -> 
        printfn "Worker"
        Coordinator.Post Ready

        let rec loop () = async {
            let! length = Coordinator.PostAndAsyncReply (fun reply -> 
                printfn "Worker: PostAndAsyncReply : %A" reply
                RequestJob reply
            )
            do! Async.Sleep length
            return! loop ()
        }
        loop ()
    )


let rand  = new System.Random(941345563)

Seq.init 500 (fun _ -> rand.Next(1,101))
|> Seq.iter (fun r -> Coordinator.Post(Job r))

let JobAgent1 = Worker ()
let JobAgent2 = Worker ()
// let JobAgent3 = Worker ()
// let JobAgent4 = Worker ()
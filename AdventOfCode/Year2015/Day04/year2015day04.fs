module Year2015Day04

open Common
open System

let hashes seed =
    Seq.initInfinite (fun index -> 
        let secretKey = String.concat "" [seed; string index]
        let data = System.Text.Encoding.ASCII.GetBytes secretKey        
        use md5 = System.Security.Cryptography.MD5.Create()
        md5.ComputeHash data
    ) 

let part1 (secretKey: string) =     
    hashes secretKey
    |> Seq.findIndex (fun hash ->
        hash.[0] = 0uy &&
        hash.[1] = 0uy &&
        hash.[2] < 16uy
    )

let part2 (secretKey: string) = 
    hashes secretKey
    |> Seq.findIndex (fun hash ->
        hash.[0] = 0uy &&
        hash.[1] = 0uy &&
        hash.[2] = 0uy
    )
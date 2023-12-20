open System.IO;
// Instructions here: https://adventofcode.com/2023/day/2
// day2.txt contains the input data from: https://adventofcode.com/2023/day/2/input
let path = "../inputs/day2.txt";
let lines = File.ReadAllLines(path);

type Cube =
    | Green of color : string * count : int
    | Red of color : string * count : int
    | Blue of color : string * count : int

type Game =
    {
        id: int 
        cubes : Cube list 
    }

let parseCubeSub (cubeStringSplit:string[]) =
    // printfn "Cube sub0: %s" cubeStringSplit.[0]
    // printfn  "Cube sub1: %s" cubeStringSplit.[1]
    let count = cubeStringSplit.[0]
    let color = cubeStringSplit.[1]
    let countAsInt = int count
    let cube = 
        match color with
        | "green" -> Green(color, countAsInt)
        | "red" -> Red(color, countAsInt)
        | "blue" -> Blue(color, countAsInt)
        | _ -> failwith "Invalid color"
    cube
    

let parseCube (cubeString:string) =
    // printfn "Cube: %s" cubeString
    let cubeStringSplit = cubeString
                            .Split(",") 
                            |> Array.map(fun x -> x.Trim().Split(" ") |> parseCubeSub) 
                            |> Array.toList
    cubeStringSplit

let parseGame (gameString:string) =
    let gameStringSplit = gameString.Split(":")
    let cubesParsed = 
        gameStringSplit.[1]
            .Split(";") 
            |> Seq.map(fun cubeString -> parseCube cubeString)
            |> List.concat
             
    let game = { 
        id = int (gameStringSplit.[0].Replace("Game ", ""))
        cubes = cubesParsed }
    // printfn "Game id: %i. cubes: %A" game.id game.cubes
    game

let isCubeCountLessThanOrEqualTo cube maxRed maxGreen maxBlue =
    match cube with
    | Green(_, count) -> count <= maxGreen
    | Red(_, count) -> count <= maxRed
    | Blue(_, count) -> count <= maxBlue
    | _ -> failwith "Invalid cube"

let isGameValid game maxRed maxGreen maxBlue =
    let cubesValid = game.cubes 
                    |> Seq.forall(fun cube -> isCubeCountLessThanOrEqualTo cube maxRed maxGreen maxBlue)
    cubesValid

let gamesValid = lines 
                |> Seq.map(fun line -> parseGame line) 
                |> Seq.filter(fun game -> isGameValid game 12 13 14) 
                |> Seq.sumBy(fun game -> game.id)
                
printfn "Games valid: %i" gamesValid

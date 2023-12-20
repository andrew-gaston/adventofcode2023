open System.IO;
// Instructions here: https://adventofcode.com/2023/day/2
// day2.txt contains the input data from: https://adventofcode.com/2023/day/2/input
let path = "../inputs/day2.txt";
let lines = File.ReadAllLines(path);

// Part 2 revealed issues with using a discriminated union for the Cube record type.
// I really just needed to be able to filter without having to pattern match. I was doing it several times.
type Cube = {
    Color: string
    Count: int
}

type Game =
    {
        id: int 
        cubes : Cube list 
    }

type GameStats =
    {
        GameID: int
        LowestRedCube: int
        LowestGreenCube: int
        LowestBlueCube: int
    }

let parseCubeSub (cubeStringSplit:string[]) =
    let cube = { 
        Count = int cubeStringSplit.[0] 
        Color = cubeStringSplit.[1]
    }
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

let isCubeCountLessThanOrEqualTo (cube: Cube, maxRed: int, maxGreen: int, maxBlue: int) =
    match cube with
    | { Color = "green"; Count = count } -> count <= maxGreen
    | { Color = "red"; Count = count } -> count <= maxRed
    | { Color = "blue"; Count = count } -> count <= maxBlue
    | _ -> failwith "Invalid cube"

let isGameValid game maxRed maxGreen maxBlue =
    let cubesValid = game.cubes 
                    |> Seq.forall(fun cube -> isCubeCountLessThanOrEqualTo(cube, maxRed, maxGreen, maxBlue))
    cubesValid

let games = lines 
                |> Seq.map parseGame
do
    games
        |> Seq.filter(fun game -> isGameValid game 12 13 14) 
        |> Seq.sumBy(fun game -> game.id)
        |> printfn "Games valid: %i"

let lowestCountOfCube cubes testColor =
    cubes
    |> Seq.filter(fun cube -> cube.Color = testColor)
    |> Seq.maxBy(fun cube -> cube.Count)
    |> fun cube -> cube.Count
        
do
    games                
        |> Seq.map(fun game -> { 
            GameID = game.id; 
            LowestRedCube = lowestCountOfCube game.cubes "red"; 
            LowestGreenCube = lowestCountOfCube game.cubes "green"; 
            LowestBlueCube = lowestCountOfCube game.cubes "blue";
        })
        |> Seq.sumBy(fun gameResult -> gameResult.LowestRedCube * gameResult.LowestGreenCube * gameResult.LowestBlueCube)
        |> printfn "Sum of Power: %i"
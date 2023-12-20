open System.IO;
// Instructions here: https://adventofcode.com/2023/day/1
// day1.txt contains the input data from: https://adventofcode.com/2023/day/1/input
let path = "../inputs/day1.txt";
let lines = File.ReadAllLines(path);

let charIsNumber (c:char) = 
    match c with
    | '0' -> true
    | '1' -> true
    | '2' -> true
    | '3' -> true
    | '4' -> true
    | '5' -> true
    | '6' -> true
    | '7' -> true
    | '8' -> true
    | '9' -> true
    | _ -> false

let replaceWordNumbersWithNumbers (line:string) = 
    line.ToLower()
        .Replace("eightwoone", "821")
        .Replace("twone", "21")
        .Replace("oneight", "18")
        // .Replace("threeight", "38") // not found in dataset
        .Replace("fiveeight", "58")
        // .Replace("sevenine", "79") // not found in dataset
        // .Replace("eighthree", "83") // not found in dataset
        .Replace("eightwo", "82")
        // .Replace("nineight", "98") // not found in dataset
        .Replace("one", "1")
        .Replace("two", "2")
        .Replace("three", "3")
        .Replace("four", "4")
        .Replace("five", "5")
        .Replace("six", "6")
        .Replace("seven", "7")
        .Replace("eight", "8")
        .Replace("nine", "9")
    
let numbersFromCharArray (numberArray:char[]) = 
    if numberArray.Length = 0 then
        // This is not reachable code with the given dataset, but it's here for completeness
        "NONE"
    else if numberArray.Length = 1 then
        let onlyNumber = numberArray.[0].ToString()
        // This was a weird rule, but it's what the problem demonstrated in the example
        // treb7uchet = 77, not just 7
        onlyNumber + onlyNumber
    else
        let firstNumber = numberArray.[0].ToString()
        let lastNumber = numberArray.[numberArray.Length - 1].ToString()
        firstNumber + lastNumber
    
let finalNumber = 
    lines 
        |> Seq.map(fun line -> replaceWordNumbersWithNumbers line)
        |> Seq.map(fun line -> line.ToCharArray()
                            |> Seq.filter(fun c -> charIsNumber c) 
                            |> Seq.toArray) 
        |> Seq.map(fun charArray -> numbersFromCharArray charArray)
        |> Seq.map(fun x -> printfn "%s" x; int x)
        |> Seq.sumBy(fun x -> x)

printfn "Final number: %i" finalNumber
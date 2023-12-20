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
    
let numbersFromCharArray (charArray:char[]) = 
    let numberArray = charArray 
                    |> Seq.filter(fun c -> charIsNumber c) 
                    |> Seq.toArray


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
    
    // I don't know if I sub-optimally wrote the pattern matching but I think the if/else is more readable
    // match numberArray.Length with
    // | 0 -> "NONE"
    // | 1 -> let onlyNumber = numberArray.[0].ToString()
    //        // This was a weird rule, but it's what the problem demonstrated in the example
    //        // treb7uchet = 77, not just 7
    //        onlyNumber + onlyNumber
    // | _ -> numberArray.[0].ToString() + numberArray.[numberArray.Length - 1].ToString()

let finalNumber = 
    lines 
        |> Seq.map(fun line -> line.ToCharArray()) 
        |> Seq.map(fun charArray -> numbersFromCharArray charArray)
        |> Seq.map(fun x -> int x)
        |> Seq.sumBy(fun x -> x)

printfn "Final number: %i" finalNumber
module LuckNov14.JS

open FunScript.Compiler
open Microsoft.FSharp.Reflection
open System
open System.IO
open System.Reflection

[<AttributeUsage(AttributeTargets.Method)>]
type JSMainAttribute(fileName: string) =
    inherit Attribute()
    member val FileName = fileName with get

let compileAll () =
    let assembly = Assembly.GetExecutingAssembly()

    let appDir = Path.GetDirectoryName(assembly.Location)
    let outputDir = Path.Combine(appDir, "Scripts")

    let toJSMain (mi: MethodInfo) =
        match mi.GetCustomAttributes(typeof<JSMainAttribute>, false) with
        | [| attr |] -> Some ((attr :?> JSMainAttribute).FileName, mi)
        | _ -> None

    let compileToFile (fileName, methodInfo) =
        let source = Compiler.Compile(Quotations.Expr.Call(methodInfo, []))
        let wrapped = sprintf "$(document).ready(function() {\n%s\n});" source
        let filePath = Path.Combine(outputDir, fileName)
        if not (Directory.Exists outputDir) then
            Directory.CreateDirectory(outputDir) |> ignore
        File.Delete(filePath)
        File.WriteAllText(filePath, wrapped)

    assembly.GetTypes()
    |> Array.filter (fun tp -> FSharpType.IsModule tp)
    |> Array.fold (fun acc mo -> mo.GetMethods() |> Array.choose toJSMain |> Array.append acc) [||]
    |> Array.iter compileToFile

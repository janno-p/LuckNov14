[<FunScript.JS>]
module Page

open FunScript
open FunScript.Compiler
open FunScript.TypeScript
open System.IO
open System.Reflection

let hello () =
    Globals.window.alert("Hello, World!")

[<LuckNov14.JS.JSMain("page.js")>]
let main () =
    hello()

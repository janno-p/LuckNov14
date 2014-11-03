[<FunScript.JS>]
module Page

open FunScript
open FunScript.Compiler
open FunScript.TypeScript
open System.IO
open System.Reflection

let hello () =
    let paper = Globals.Raphael.Invoke(10.0, 50.0, 320.0, 200.0)
    let circle = paper.circle(50.0, 40.0, 10.0)
                      .attr("fill", "#f00")
                      .attr("stroke", "#fff")
    ()

[<LuckNov14.JS.JSMain("page.js")>]
let main () =
    hello()

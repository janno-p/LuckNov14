[<FunScript.JS>]
module Page

open FunScript
open FunScript.Compiler
open FunScript.TypeScript
open System.Collections.Generic
open System.IO
open System.Reflection

let add key value (x: Dictionary<_,_>) =
    x.Add(key, box value)
    x

[<JSEmitInline("{0}")>]
let asFunction (f: 'a -> 'b): Function = failwith "never"

type SimpleGame () =
    let preload (game: Phaser.Game) =
        game.load.image("logo", "Images/phaser.png")

    let create (game: Phaser.Game) =
        let logo = game.add.sprite(game.world.centerX, game.world.centerY, "logo")

        logo.anchor.setTo(0.5, 0.5) |> ignore
        logo.scale.setTo(0.2, 0.2) |> ignore

        let twopt = Dictionary<_, obj>() |> add "x" 1 |> add "y" 1
        game.add.tween(logo.scale)._to(twopt, 2000.0, Phaser.Easing.Bounce.Out |> asFunction, true)

    let options = Dictionary<_, obj>()
                  |> add "preload" preload
                  |> add "create" create

    let game = Phaser.Game.Create(800.0, 600.0, Phaser_.AUTO, "", options)

[<LuckNov14.JS.JSMain("page.js")>]
let main () = SimpleGame()

[<FunScript.JS>]
module Page

open FunScript
open FunScript.Compiler
open FunScript.TypeScript
open System.Collections.Generic
open System.IO
open System.Reflection

let add key value (x: Dictionary<_,_>) =
    x.Add(key, value)
    x

[<LuckNov14.JS.JSMain("page.js")>]
let main () =
    let preload (game: Phaser.Game) =
        game.load.image("logo", "Images/phaser.png")

    let create (game: Phaser.Game) =
        let logo = game.add.sprite(game.world.centerX, game.world.centerY, "logo")
        logo.anchor.setTo(0.5, 0.5)

    let options = Dictionary<_, obj>()
                  |> add "preload" (box preload)
                  |> add "create" (box create)

    Phaser.Game.Create(800.0, 600.0, PhaserClass.AUTO, "", options)

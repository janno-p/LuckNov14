open Suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Files
open Suave.Http.RequestErrors
open Suave.Http.Successful
open Suave.Types
open Suave.Web

printfn "tere"

LuckNov14.JS.compileAll()

(*
let default_mime_types_map = function
    | ".js" -> mk_mime_type "application/x-javascript" true
    | _     -> None
*)

let app : WebPart =
    choose [
        url "/page" >>= OK """<!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                <title>LuckNov14</title>
            </head>
            <body>
                <script src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
                <script src="/Scripts/page.js"></script>
            </body>
            </html>"""
        GET >>= url_regex "/Scripts/(.*)\.js$" >>= browse
        NOT_FOUND "Found no handlers"
    ]

web_server default_config app

open Suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Files
open Suave.Http.RequestErrors
open Suave.Http.Successful
open Suave.Types
open Suave.Web
open System.Text

LuckNov14.JS.compileAll()

let content = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>LuckNov14</title>
    <script src=""/Scripts/phaser.min.js""></script>
</head>
<body>
    <script src=""/Scripts/page.js""></script>
</body>
</html>"

let app : WebPart =
    choose [
        url "/page" >>= OK content
        GET >>= url_regex "/Scripts/(.*)\.js$" >>= browse'
        GET >>= url_regex "/Scripts/(.*)\.map$" >>= browse'
        GET >>= url_regex "/Images/(.*)\.png$" >>= browse'
        NOT_FOUND "Found no handlers"
    ]

let mime_types =
  default_config.mime_types_map
    >=> (function | ".map" -> Suave.Http.Writers.mk_mime_type "application/json" false | _ -> None)

web_server { default_config with mime_types_map = mime_types } app

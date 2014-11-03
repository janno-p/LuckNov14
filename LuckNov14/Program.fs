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
</head>
<body style=""background-color: #ccc;"">
    <script src=""https://code.jquery.com/jquery-2.1.1.min.js""></script>
    <script src=""//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.2/raphael-min.js""></script>
    <script src=""/Scripts/page.js""></script>
</body>
</html>"

let app : WebPart =
    choose [
        url "/page" >>= OK content
        GET >>= url_regex "/Scripts/(.*)\.js$" >>= browse
        NOT_FOUND "Found no handlers"
    ]

web_server default_config app

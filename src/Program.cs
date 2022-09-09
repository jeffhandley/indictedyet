using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Cryptography;
using static System.Text.Encoding;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("{criminal=the-former-guy}", (HttpContext context, string criminal) => {
    string[] noEmojis = new[] { "😒","🤨","😤","😡","🤬","😾","😑","🙄","😣","😫","😖","😞","😟","😧","😩" };
    string randomNoEmoji = noEmojis[RandomNumberGenerator.GetInt32(noEmojis.Length)];

    string notYet = $"No, not yet.<p>{randomNoEmoji}</p>";
    string no = $"No, and they won't be.<p>{randomNoEmoji}</p>";

    var name = criminal switch {
        "the-former-guy" or "tfg" or "45" or "www" or "" or null => "The Former Guy",
        "steve-bannon" or "bannon" => "Steve Bannon",
        "michael-flynn" or "flynn" => "Michael Flynn",
        "matt-gaetz" or "gaetz" or "rapey-mcforehead" => "Rapey McForehead",
        _ => criminal
    };

    var haveThey = name switch {
        "The Former Guy" => notYet,
        "Michael Flynn" => notYet,
        "Rapey McForehead" => notYet,
        "Steve Bannon" => @"Yes! He was <a target=""story"" href=""https://www.pbs.org/newshour/politics/steve-bannon-pleads-not-guilty-to-laundering-money-donated-to-build-border-wall"">indicted in New York on September 8, 2022</a> for defrauding MAGA supporters out of 'We&nbsp;Build&nbsp;the&nbsp;Wall' money.<p>🥳</p><p>Unfortunately, he was released without bail.</p>",
        _ => @"Not that we know of. Tweet <a target=""share"" href=""https://twitter.com/IndictedYet"">@IndictedYet</a> if you have an update!"
    };

    context.Response.ContentType = "text/html; charset=utf-8";

    return @"
<!DOCTYPE html>
<html lang=""en-US"">
    <head>
        <meta charset=""utf-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
        <link rel=""apple-touch-icon"" sizes=""180x180"" href=""/apple-touch-icon.png"" />
        <link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""/favicon-32x32.png"" />
        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/favicon-16x16.png"" />
        <link rel=""manifest"" href=""/site.webmanifest"" />
        <title>" +
            $"Is {name} Indicted Yet?" + @"
        </title>
        <style>
            html {
                width: 100vw;
                height: 100vh;
                background-color: #0C0C0C;
                background-image: url(/jail-bg.jpg);
                background-repeat: no-repeat;
                background-size: cover;
                color: rgb(25, 25, 25);
                font-family: sans-serif;
            }
            body {
                margin: 0;
                padding: 0;
            }
            #body-content {
                background-color: rgba(225, 225, 255, 0.4);
                border: 1px solid black;
                border-radius: 25px;
                margin: 5%;
                padding: 2em;
                text-align: center;
            }
            #body-content h1 {
                font-size: 300%;
            }
            #body-content h2 {
                font-size: 180%;
            }
            #share {
                min-height: 40px;
                padding-top: 25px;
                transition: opacity 0.3s;
                opacity: 1;
            }
            #foot-content {
                background-color: rgba(225, 225, 255, 0.75);
                border-top: 2px solid black;
                position: absolute;
                bottom: 0;
                margin: 0;
                width: 100%;
                padding: 0.6em 0;
                text-align: center;
                font-weight: bold;
            }
            a, a:visited, a:active {
                color: rgb(25, 25, 175);
            }
            .invisible {
                opacity: 0!important;
            }
        </style>
    </head>
    <body>
        <div id=""body-content"">
            <h1>" +
                $"Is {name} Indicted Yet?" + @"
            </h1>
            <h2>" +
                haveThey + @"
            </h2>
            <div id=""share"" class=""invisible"">
                <a href=""https://twitter.com/share?ref_src=twsrc%5Etfw"" class=""twitter-share-button"" data-size=""large"" data-text=""" + $"Is {name} @IndictedYet?" + @""" data-related=""IndictedYet"" data-show-count=""true"">Tweet</a><script async src=""https://platform.twitter.com/widgets.js"" charset=""utf-8""></script>
            </div>
        </div>
        <div id=""foot-content"">
            Website by <a target=""twitter"" href=""https://twitter.com/JeffHandley"">@JeffHandley</a>.
            Photo by <a target=""unsplash"" href=""https://unsplash.com/@emilianobar?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"">Emiliano Bar</a> on <a target=""unsplash"" href=""https://unsplash.com/s/photos/prison?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"">Unsplash</a>.
        </div>
    </body>
    <script>
        window.setTimeout(() => { document.getElementById(""share"").className = """"; console.log(document.getElementById(""share"").className);}, 250);
    </script>
</html>
";
});

app.Run();

using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Cryptography;
using static System.Text.Encoding;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("{name=the-former-guy}", (HttpContext context, string name) => {
    string[] notIndictedEmojis = new[] { "😒","🤨","😤","😡","🤬","😾","😑","🙄","😣","😫","😖","😞","😟","😧","😩" };
    string[] indictedEmojis = new[] { "🙂","😏","😆","😅","😀","🎉","🔥","😁","😂","🤣","😃","😄","😶‍🌫️","😝","😛","🤪","🥳"};

    string randomNotIndictedEmoji = notIndictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)];
    string randomIndictedEmoji = indictedEmojis[RandomNumberGenerator.GetInt32(indictedEmojis.Length)];

    string notYet = @$"No, not yet.<p class=""emoji"">{randomNotIndictedEmoji}</p>";
    string no = @$"No, and they won't be.<p class=""emoji"">{randomNotIndictedEmoji}</p>";

    name = name.ToLower();

    var autocorrect = new Dictionary<string, string> {
        { "matt-geatz", "matt-gaetz" },
        { "geatz", "matt-gaetz" },
        { "theformerguy", "the-former-guy" },
        { "rapeymcforehead", "matt-gaetz" }
    };

    autocorrect.TryGetValue(name, out var correctName);
    name = correctName ?? name;

    var aliases = new Dictionary<string, string> {
        { "the-former-guy", "the-former-guy" },
        { "tfg", "the-former-guy" },
        { "45", "the-former-guy" },
        { "steve-bannon", "steve-bannon" },
        { "bannon", "steve-bannon" },
        { "michael-flynn", "michael-flynn" },
        { "flynn", "michael-flynn" },
        { "matt-gaetz", "matt-gaetz" },
        { "gaetz", "matt-gaetz" },
        { "rapey-mcforehead", "matt-gaetz" }
    };

    aliases.TryGetValue(name, out var criminalName);
    name = criminalName ?? name;

    var criminal = name switch {
        "the-former-guy" => new Criminal {
            Name = "The Former Guy",
            Url = "https://twitter.com/search?q=%23TFG&f=live",
            Message = notYet
        },
        "steve-bannon" => new Criminal {
            Name = "Steve Bannon",
            Url = "https://twitter.com/search?q=steve%20bannon%20perp%20walk&f=video",
            Message = @$"Yes! He was <a target=""story"" href=""https://www.pbs.org/newshour/politics/steve-bannon-pleads-not-guilty-to-laundering-money-donated-to-build-border-wall"">indicted in New York on September 8, 2022</a> for defrauding MAGA supporters out of 'We&nbsp;Build&nbsp;the&nbsp;Wall' money.<p class=""emoji"">{randomIndictedEmoji}</p><p>Unfortunately, he was released without bail.</p><p class=""emoji"">{randomNotIndictedEmoji}</p>"
        },
        "michael-flynn" => new Criminal {
            Name = "Michael Flynn",
            Url = "https://twitter.com/search?q=Michael%20Flynn&f=live",
            Message = notYet
        },
        "matt-gaetz" => new Criminal {
            Name = "Matt Gaetz",
            Url = "https://twitter.com/search?q=%23RapeyMcForehead&f=live",
            Message = notYet
        },
        _ => new Criminal {
            Name = name,
            Message = @"Not that we know of. <a target=""github"" href=""https://github.com/jeffhandley/indictedyet/edit/main/src/Program.cs"">Submit a contribution</a> if you have an update!"
        }
    };

    var linkedName = criminal.Url is not null ?
        $@"<a target=""twitter"" href=""{criminal.Url}"">{criminal.Name}</a>" :
        criminal.Name;

    var suggestions = aliases.Where(alias => alias.Value != criminalName).Select(alias => alias.Key);
    var suggestedCriminal = suggestions.ElementAt(RandomNumberGenerator.GetInt32(suggestions.Count()));

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
            $"Is {criminal.Name} Indicted Yet?" + @"
        </title>
        <style>
            html {
                width: 100%;
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
            #suggestion {
                font-size: xx-large;
                font-weight: bold;
            }
            #foot-content {
                background-color: rgba(225, 225, 255, 0.75);
                border-top: 2px solid black;
                margin-top: 15vh;
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
            .emoji {
                font-size: xxx-large;
            }
        </style>
    </head>
    <body>
        <div id=""body-content"">
            <h1>" +
                $"Is {linkedName} Indicted Yet?" + @"
            </h1>
            <h2>" +
                criminal.Message + @"
            </h2>
            <div id=""share"" class=""invisible"">
                <a href=""https://twitter.com/share?ref_src=twsrc%5Etfw"" class=""twitter-share-button"" data-size=""large"" data-text=""" + $"Is {criminal.Name} @IndictedYet?" + @""" data-related=""IndictedYet"" data-show-count=""true"">Tweet</a><script async src=""https://platform.twitter.com/widgets.js"" charset=""utf-8""></script>
            </div>
            <p id=""suggestion"">
                " + @$"What about <a href=""{suggestedCriminal}"">{suggestedCriminal}</a>?" + @"
            </p>
        </div>
        <div id=""foot-content"">
            <a href=""github"" href=""https://github.com/jeffhandley/indictedyet"">Website</a> by <a target=""twitter"" href=""https://twitter.com/JeffHandley"">@JeffHandley</a>.
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

struct Criminal
{
    public string Name;
    public string Url;
    public string Message;
}

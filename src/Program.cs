using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using System.Security.Cryptography;
using static System.Text.Encoding;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("{name=the-former-guy}", (HttpContext context) => {
    var name = ((string)context.GetRouteValue("name")).ToLower();

    var redirects = new Dictionary<string, string> {
        { "theformerguy", "the-former-guy" },
        { "45", "the-former-guy" },
        { "mattgaetz", "matt-gaetz" },
        { "gaetz", "matt-gaetz" },
        { "matt-geatz", "matt-gaetz" },
        { "geatz", "matt-gaetz" },
        { "rapeymcforehead", "rapey-mcforehead" },
        { "stevebannon", "steve-bannon" },
        { "bannon", "steve-bannon" },
        { "michaelflynn", "michael-flynn" },
        { "flynn", "michael-flynn" },
        { "billbarr", "bill-barr" },
        { "barr", "bill-barr" },
        { "bar", "bill-barr" },
        { "junior", "donjr" },
        { "jr", "donjr" },
        { "don-jr", "donjr" },
        { "don-junior", "donjr" },
        { "rogerstone", "roger-stone" },
        { "roger", "roger-stone" },
        { "stone", "roger-stone" },
        { "ginni", "ginni-thomas" },
        { "clarence", "clarence-thomas" },
    };

    if (redirects.TryGetValue(name, out var redirection)) {
        context.Response.Redirect($"/{redirection}", true, true);
        name = redirection;
    }

    var aliases = new Dictionary<string, string> {
        { "the-former-guy", "the-former-guy" },
        { "tfg", "the-former-guy" },
        { "steve-bannon", "steve-bannon" },
        { "michael-flynn", "michael-flynn" },
        { "matt-gaetz", "matt-gaetz" },
        { "rapey-mcforehead", "matt-gaetz" },
        { "bill-barr", "bill-barr" },
        { "ivanka", "ivanka" },
        { "donjr", "donjr" },
        { "roger-stone", "roger-stone" },
        { "ginni-thomas", "ginni-thomas" },
        { "clarence-thomas", "clarence-thomas" },
        { "stephen-miller", "stephen-miller" },
        { "miller", "stephen-miller" },
    };

    aliases.TryGetValue(name, out var criminalName);
    name = criminalName ?? name;

    string[] notIndictedEmojis = new[] { "😒","🤨","😤","😡","🤬","😾","😑","🙄","😣","😫","😖","😞","😟","😧","😩" };
    string[] indictedEmojis = new[] { "🙂","😏","😆","😅","😀","🎉","🔥","😁","😂","🤣","😃","😄","😶‍🌫️","😝","😛","🤪","🥳"};

    string randomNotIndictedEmoji = notIndictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)];
    string randomIndictedEmoji = indictedEmojis[RandomNumberGenerator.GetInt32(indictedEmojis.Length)];

    string notYet = @$"No, not yet.<p class=""emoji delayed-visibility"">{randomNotIndictedEmoji}</p>";
    string no = @$"No, and they won't be.<p class=""emoji delayed-visibility"">{randomNotIndictedEmoji}</p>";

    var criminal = name switch {
        "the-former-guy" => new Criminal {
            Name = "The Former Guy",
            Url = "https://twitter.com/search?q=%23TFG&f=live",
            Message = notYet
        },
        "steve-bannon" => new Criminal {
            Name = "Steve Bannon",
            Url = "https://twitter.com/search?q=Steve%20Bannon%20perp%20walk&f=video",
            Message = @$"Yes! He was <a target=""story"" href=""https://www.pbs.org/newshour/politics/steve-bannon-pleads-not-guilty-to-laundering-money-donated-to-build-border-wall"">indicted in New York on September 8, 2022</a> for defrauding MAGA supporters out of 'We&nbsp;Build&nbsp;the&nbsp;Wall' money." +
                      @"<div id=""embedded-tweet"" class=""delayed-visibility"">" +
                      @"<blockquote class=""twitter-tweet""><p lang=""en"" dir=""ltr"">Perp walk for Steve Bannon. The far-right former Trump aide, who is charged with money-laundering, conspiracy and fraud, seems as delusional as ever. <a href=""https://t.co/UDwIgIel7C"">pic.twitter.com/UDwIgIel7C</a></p>&mdash; Ian Fraser (@Ian_Fraser) <a href=""https://twitter.com/Ian_Fraser/status/1568297092124413953?ref_src=twsrc%5Etfw"">September 9, 2022</a></blockquote> <script async src=""https://platform.twitter.com/widgets.js"" charset=""utf-8""></script>" +
                      @"</div>" +
                      @$"<p>Unfortunately, he was released without bail.</p><p class=""emoji delayed-visibility"">{randomNotIndictedEmoji}</p>"
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
        "bill-barr" => new Criminal {
            Name = "Bill Barr",
            Url = "https://twitter.com/search?q=Bill%20Barr&f=live",
            Message = notYet
        },
        "ivanka" => new Criminal {
            Name = "Ivanka",
            Url = "https://twitter.com/search?q=%23Ivanka&f=live",
            Message = notYet
        },
        "donjr" => new Criminal {
            Name = "Don Jr.",
            Url = "https://twitter.com/search?q=%23DonJr&f=live",
            Message = notYet
        },
        "roger-stone" => new Criminal {
            Name = "Roger Stone",
            Url = "https://twitter.com/search?q=%23RogerStone&f=live",
            Message = notYet
        },
        "ginni-thomas" => new Criminal {
            Name = "Ginni Thomas",
            Url = "https://twitter.com/search?q=%23GinniThomas&f=live",
            Message = notYet
        },
        "clarence-thomas" => new Criminal {
            Name = "Clarence Thomas",
            Url = "https://twitter.com/search?q=%23ClarenceThomas&f=live",
            Message = notYet
        },
        "stephen-miller" => new Criminal {
            Name = "Stephen Miller",
            Url = "https://twitter.com/search?q=%23StephenMiller&f=live",
            Message = @$"Not yet, but he was <a target=""story"" href=""https://www.dailymail.co.uk/news/article-11198927/Stephen-Miller-dozen-Trump-associates-hit-subpoenas.html"">subpoenaed by the grand jury on September 9, 2022</a>!<p class=""emoji delayed-visibility"">{randomIndictedEmoji}</p>"
        },
        _ => new Criminal {
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Replace("-", " ")),
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
                transition: opacity 0.3s;
                transition-delay: 0.2s;
            }
            #body-content h2 {
                font-size: 180%;
                transition: opacity 0.3s;
                transition-delay: 1.0s;
            }
            #share {
                min-height: 40px;
                padding-top: 10px;
                transition: opacity 0.3s;
                transition-delay: 1.8s;
            }
            #embedded-tweet {
                min-height: 600px;
                padding-top: 25px;
                text-align: -webkit-center;
                transition: opacity 0.3s;
                transition-delay: 3.0s;
            }
            #suggestion {
                font-size: xx-large;
                font-weight: bold;
                transition: opacity 0.3s;
                transition-delay: 2.0s;
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
            .delayed-visibility {
                opacity: 0!important;
            }
            .emoji {
                font-size: xxx-large;
                transition: opacity 0.3s;
                transition-delay: 1.3s;
            }
        </style>
    </head>
    <!-- Google tag (gtag.js) -->
    <script async src=""https://www.googletagmanager.com/gtag/js?id=G-1CEGWC1TSR""></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag(){dataLayer.push(arguments);}
        gtag('js', new Date());
        gtag('config', 'G-1CEGWC1TSR');
    </script>
    <body>
        <div id=""body-content"">
            <h1 class=""delayed-visibility"">" +
                $"Is {linkedName} Indicted Yet?" + @"
            </h1>
            <h2 class=""delayed-visibility"">" +
                criminal.Message + @"
            </h2>
            <div id=""share"" class=""delayed-visibility"">
                <a href=""https://twitter.com/share?ref_src=twsrc%5Etfw"" class=""twitter-share-button"" data-size=""large"" data-text=""" + $"Is {criminal.Name} @IndictedYet?" + @""" data-related=""IndictedYet"" data-show-count=""true"">Tweet</a><script async src=""https://platform.twitter.com/widgets.js"" charset=""utf-8""></script>
            </div>
            <p id=""suggestion"" class=""delayed-visibility"">
                " + @$"What about <a href=""{suggestedCriminal}"">{suggestedCriminal}</a>?" + @"
            </p>
        </div>
        <div id=""foot-content"">
            <a target=""github"" href=""https://github.com/jeffhandley/indictedyet"">Website</a> by <a target=""twitter"" href=""https://twitter.com/JeffHandley"">@JeffHandley</a>.
            Photo by <a target=""unsplash"" href=""https://unsplash.com/@emilianobar?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"">Emiliano Bar</a> on <a target=""unsplash"" href=""https://unsplash.com/s/photos/prison?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText"">Unsplash</a>.
        </div>
    </body>
    <script>
        window.setTimeout(() => [...document.getElementsByClassName(""delayed-visibility"")].forEach(e => e.classList.remove(""delayed-visibility"")), 250);
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

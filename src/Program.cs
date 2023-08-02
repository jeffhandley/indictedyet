using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using System.Security.Cryptography;
using static System.Text.Encoding;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("{name=the-former-guy}", (HttpContext context) => {
    var name = ((string)context.GetRouteValue("name")!).ToLower();

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
        { "miller", "stephen-miller" },
        { "gohmert", "louie-gohmert" },
        { "gomert", "louie-gohmert" },
        { "gingrich", "newt-gingrich" },
        { "newt", "newt-gingrich" },
        { "graham", "lindsey-graham" },
        { "lindsey", "lindsey-graham" },
        { "kushner", "jared-kushner" },
        { "meadows", "mark-meadows" },
        { "abbott", "greg-abbott" },
        { "abbot", "greg-abbott" },
        { "greg-abbot", "greg-abbott" },
        { "desantis", "ron-desantis" },
        { "marjorie-taylor-greene", "mtg" },
        { "marjorie-taylor-green", "mtg" },
    };

    if (redirects.TryGetValue(name, out var redirection)) {
        context.Response.Redirect($"/{redirection}");
        name = redirection;
    }

    var aliases = new Dictionary<string, string> {
        { "tfg", "the-former-guy" },
        { "rapey-mcforehead", "matt-gaetz" },
        { "gym-jordan", "jim-jordan" },
        { "pillow-guy", "mike-lindell" },
        { "the-pillow-guy", "mike-lindell" },
        { "marjorie-nazi-greene", "mtg" },
    };

    aliases.TryGetValue(name, out var criminalName);
    name = criminalName ?? name;

    string[] notIndictedEmojis = new[] { "😒","🤨","😤","😡","🤬","😾","😑","🙄","😣","😫","😖","😞","😟","😧","😩" };
    string[] indictedEmojis = new[] { "🙂","😏","😆","😅","😀","🎉","🔥","😁","😂","🤣","😃","😄","😶‍🌫️","😝","😛","🤪","🥳"};

    string randomNotIndictedEmoji = notIndictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)];
    string randomIndictedEmoji = indictedEmojis[RandomNumberGenerator.GetInt32(indictedEmojis.Length)];

    string notYet = $"""
        No, not yet.<p class="emoji delayed-visibility">{randomNotIndictedEmoji}</p>
        """;

    string no = $"""
        No, and they won't be.<p class="emoji delayed-visibility">{randomNotIndictedEmoji}</p>
        """;

    string yes = $"""
        YES!
        <p class="emoji delayed-visibility">{randomIndictedEmoji}</p>
        """;

    var criminals = new Dictionary<string, Criminal> {
        { "the-former-guy", new Criminal {
            Name = "The Former Guy",
            Hashtag = "TFG",
            Heading = $"""
                YES! There are four sets of indictments so far.
                """,
            Body = $"""
                <div>
                    <p>
                        Tuesday, August 1st, the Justice Department <a target="story" href="https://www.pbs.org/newshour/politics/watch-live-trump-indicted-on-federal-charges-in-jan-6-case-special-counsel-jack-smith-announces">indicted him on four felony counts related to his attempts to overturn the 2020 election</a>. He is finally being held responsible and accountable for the January 6th attack on the Capitol and other efforts to prevent the transfer of power. This is the day we've all been waiting for since even before the January 6th riots.
                    </p>
                    <p>
                        Earlier in this process, his lawyers <a target="story" href="https://apnews.com/article/trump-justice-department-investigation-indictment-730455c2b7a684593e41362e4b661cb3">met with Jack Smith's team</a> on July 27 to discuss the pending indictment, but <a target="story" href="https://www.independent.co.uk/news/world/americas/us-politics/trump-indictment-today-special-counsel-watch-b2383238.html">no charges will be filed today</a>.
                    </p>
                    <p>
                        On Sunday, July 16th, he received a letter from special counsel Jack Smith that <a target="story" href="https://www.theguardian.com/us-news/2023/jul/19/trump-investigation-three-federal-crimes-january-6">he is the TARGET of federal charges</a> related to the January 6th coup attempt.
                    </p>
                    <p class="emoji delayed-visibility">{indictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)]}</p>
                </div>
                <hr />
                <div>
                    <p>Whoa; we got a consolation indictment! On Thursday, July 27, he was charged with <a target="story" href="https://www.washingtonpost.com/national-security/2023/07/27/trump-carlos-deoliveira-classified-indictment/">seeking to delete security footage</a> in the documents case. This is a fresh indictment on top of the existing classified documents handling charges.</p>
                    <p class="emoji delayed-visibility">{indictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)]}</p>
                </div>
                <hr />
                <div>
                    <p>On Thursday, June 8, 2023, the Justice Department <a target="story" href="https://www.reuters.com/world/us/trump-biden-administration-has-informed-my-lawyers-that-ive-been-indicted-2023-06-08/">indicted him on 7 counts</a> related to confidential documents mishandling after leaving office.</p>
                    <p>He was arraigned and presented with his charges on July 13, 2023. Instead of just 7 counts, he was <a target="story" href="https://www.pbs.org/newshour/politics/live-updates-trumps-federal-arraignment-in-miami">presented with 37 felony counts</a> related to hoarding top secret government documents, boastfully displaying them to visitors and trying to hide them from investigators who demanded them back.</p>
                    <p class="emoji delayed-visibility">{indictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)]}</p>
                </div>
                <hr />
                <div>
                    <p>On Tuesday, April 4, 2023, he was <a target="story" href="https://www.pbs.org/newshour/politics/donald-trump-indicted-lawyer-says">arraigned and charged with 34 felony counts of fraud</a> and falsifying records to influence the 2016 election. He pleaded not guilty to all counts.</p>
                    <p class="emoji delayed-visibility">{indictedEmojis[RandomNumberGenerator.GetInt32(notIndictedEmojis.Length)]}</p>
                </div>
                """
        } },
        { "michigan-16", new Criminal {
            Name = "Michigan's Group of 16 False Electors",
            Hashtag = "Michigan False Electors",
            Heading = yes,
            Body = $"""
                <p>On July 18, Michigan Attorney General Dana Nessel 
                <a target="story" href="https://www.npr.org/2023/07/18/1188437637/michigans-fake-elector-charges">announced felony charges</a>
                against 16 Michigan residents for their role in the alleged false electors scheme
                following the 2020 U.S. presidential election.
                """
        } },
        { "steve-bannon", new Criminal {
            Name = "Steve Bannon",
            Heading = yes,
            Body = $"""
                <p>On October 21, 2022 (the same day <a href="/tfg">the former guy was subpoenaed</a>), Bannon was <a target="story" href="https://apnews.com/article/capitol-siege-steve-bannon-congress-donald-trump-sentencing-36d412eba9e1609a030859852378ae3d">sentenced to 4 months behind bars</a> for defying a subpoena. Plus, he was <a target="story" href="https://www.pbs.org/newshour/politics/steve-bannon-pleads-not-guilty-to-laundering-money-donated-to-build-border-wall">indicted in New York</a> on September 8, 2022 for defrauding MAGA supporters out of 'We&nbsp;Build&nbsp;the&nbsp;Wall' money.</p>
                """
        } },
        { "george-santos", new Criminal {
            Name = "Rep. George Santos",
            Heading = yes,
            Body = $"""
                <p>On May 10, 2023, <a target="story" href="https://apnews.com/hub/george-santos">George Santos</a>
                    <a target="story" href="https://apnews.com/article/george-santos-justice-department-new-york-7e16d39eea0fc577f78d17502a384084">was indicted</a>
                    and faced <a target="story" href="https://apnews.com/article/george-santos-charges-explanation-b3469ff891e0fa53ea78fa15910ebc62">13 counts related to fraud, embezzlement, and false statements</a>.
                    He <a target="story" href="https://apnews.com/article/george-santos-federal-charges-updates-33667a0900271e5002459ab748d8fdc8?utm_source=homepage&utm_medium=TopNews&utm_campaign=position_01">pleaded not guilty</a>, of course.
                </p>
                """
        } },
        { "michael-flynn", new Criminal("Michael Flynn") },
        { "matt-gaetz", new Criminal {
            Name = "Matt Gaetz",
            Hashtag = "#RapeyMcForehead",
            Heading = no
        } },
        { "bill-barr", new Criminal("Bill Barr") },
        { "ivanka", new Criminal("Ivanka") },
        { "donjr", new Criminal("Don Jr") },
        { "roger-stone", new Criminal("Roger Stone") },
        { "ginni-thomas", new Criminal("Ginni Thomas") },
        { "clarence-thomas", new Criminal("Clarence Thomas") },
        { "stephen-miller", new Criminal {
            Name = "Stephen Miller"
        } },
        { "louie-gohmert", new Criminal("Louie Gohmert") },
        { "newt-gingrich", new Criminal("Newt Gingrich") },
        { "jim-jordan", new Criminal {
            Name = "Jim Jordan",
            Hashtag = "#GymJordan"
        } },
        { "lindsey-graham", new Criminal("Lindsey Graham") },
        { "jared-kushner", new Criminal("Jared Kushner") },
        { "mark-meadows", new Criminal("Mark Meadows") },
        { "phil-bryant", new Criminal("Gov. Phil Bryant") { Hashtag = "Gov. Phil Bryant" } },
        { "brett-favre", new Criminal("Brett Favre") },
        { "mike-lindell", new Criminal("Mike Lindell, the pillow guy") {
            Hashtag = "#MikeLindell"
        } },
        { "greg-abbott", new Criminal("Gov. Greg Abbott") { Hashtag = "Gov. Greg Abbott" } },
        { "ron-desantis", new Criminal("Gov. Ron Desantis") { Hashtag = "Gov. Ron DeSantis" } },
        { "mtg", new Criminal("Rep. Marjorie Taylor Greene") { Hashtag = "MTG" } },
        { "jeffrey-clark", new Criminal("Jeffrey Clark") },
        { "hunter-biden", new Criminal {
            Name = "Hunter Biden",
            Heading = yes,
            Body = $"""
                <p>Well, how about that? On June 20, 2023, <a target="story" href="https://www.npr.org/2023/06/20/1087173827/hunter-biden">
                he agreed to plead guilty</a> for charges related to his federal income taxes, along with a firearm offense charge. He did
                not pay federal income taxes in 2017 or 2018, despite owing more than $100,000 each of those years.</p>
                """
        } },
    };

    var defaultCriminal = new Criminal {
        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Replace("-", " ")),
        Heading = "Not that we know of."
    };

    if (!criminals.TryGetValue(name, out var criminal)) {
        criminal = defaultCriminal;
    }

    criminal.Hashtag ??= "#" + criminal.Name.Replace(" ", "").Replace(".", "");
    criminal.Heading ??= notYet;
    criminal.Body ??= $"""
            <p><a target="github" href="https://github.com/jeffhandley/indictedyet/edit/main/src/Program.cs">Submit a contribution</a> if you have an update!</p>
            """;

    var suggestions = criminals.Where(c => c.Key != criminalName).Select(c => c.Key);
    var suggestedAlias = suggestions.ElementAt(RandomNumberGenerator.GetInt32(suggestions.Count()));
    var suggestedCriminalName = criminals[suggestedAlias].Name;

    context.Response.ContentType = "text/html; charset=utf-8";

    return $$"""
        <!DOCTYPE html>
        <html lang="en-US">
            <head>
                <meta charset="utf-8">
                <meta name="viewport" content="width=device-width, initial-scale=1" />
                <link rel="apple-touch-icon" sizes="180x180" href="/apple-touch-icon.png" />
                <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png" />
                <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png" />
                <link rel="manifest" href="/site.webmanifest" />
                <title>Is {{criminal.Name}} Indicted Yet?</title>
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
                    #body-content p {
                        font-size: 130%;
                        font-weight: bold;
                    }
                    #share {
                        min-height: 40px;
                        padding-top: 10px;
                        transition: opacity 0.3s;
                        transition-delay: 1.8s;
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
            <script async src="https://www.googletagmanager.com/gtag/js?id=G-1CEGWC1TSR"></script>
            <script>
                window.dataLayer = window.dataLayer || [];
                function gtag(){dataLayer.push(arguments);}
                gtag('js', new Date());
                gtag('config', 'G-1CEGWC1TSR');
            </script>
            <body>
                <div id="body-content">
                    <h1 class="delayed-visibility">Is {{criminal.Name}} Indicted Yet?</h1>
                    <h2 class="delayed-visibility">{{criminal.Heading}}</h2>
                    <hr />
                    <div class="delayed-visibility">
                        {{criminal.Body}}
                    </div>
                    <hr />
                    <p id="suggestion" class="delayed-visibility">
                        What about <a href="{{suggestedAlias}}">{{suggestedCriminalName}}</a>?
                    </p>
                </div>
                <div id="foot-content">
                    <a target="github" href="https://github.com/jeffhandley/indictedyet">Website by Jeff Handley</a>.
                    Photo by <a target="unsplash" href="https://unsplash.com/@emilianobar?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Emiliano Bar</a> on <a target="unsplash" href="https://unsplash.com/s/photos/prison?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Unsplash</a>.
                </div>
            </body>
            <script>
                window.setTimeout(() => [...document.getElementsByClassName("delayed-visibility")].forEach(e => e.classList.remove("delayed-visibility")), 250);
            </script>
        </html>
        """;
});

app.Run();

struct Criminal
{
    public string Name;
    public string? Hashtag;
    public string? Url;
    public string? Heading;
    public string? Body;

    public Criminal(string name) {
        Name = name;
        Hashtag = null;
        Url = null;
        Heading = null;
        Body = null;
    }
}

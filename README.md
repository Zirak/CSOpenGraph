A small OpenGraph data parser. Contains 3 main methods, of which you will only realistically use two:

```csharp
OpenGraph og = new OpenGraph();
OpenGraphData data;
//to get the data, one of:
data = og.ParseFromUrl(url); //from internet
data = og.ParseFromPath(filePath); //from file
data = og.ParseDocument(htmlDocument); //from already built HtmlDocument
```

The code is small and relatively clean. `OpenGraphData` is really just a `Dictionary<string, string>`, so juggling with it should prove easy.

Example of parsing http://www.imdb.com/title/tt0379786/ :

```csharp
OpenGraphData data = (new OpenGraph()).ParseFromUrl("http://www.imdb.com/title/tt0379786/");

foreach (KeyValuePair<string, string> pair in data) {
    Console.WriteLine("{0} => {1}", pair.Key, pair.Value);
}
```

Gives you:

    url => http://www.imdb.com/title/tt0379786/
    image => http://ia.media-imdb.com/images/M/MV5BMTI0NTY1MzY4NV5BMl5BanBnXkFtZTcwNTczODAzMQ@@._V1_SY317_CR0,0,214,317_.jpg
    type => video.movie
    title => Serenity (2005)
    site_name => IMDb
    description => Directed by Joss Whedon.  With Nathan Fillion, Gina Torres, Alan Tudyk, Chiwetel Ejiofor. The crew of the ship Serenity tries to evade an assassin sent to recapture one of their number who is telepathic.

Tested for

* Facebook
* Github
* IMDB
* SourceForge
* Think-Geek
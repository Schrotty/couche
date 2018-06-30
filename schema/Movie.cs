using System;
using System.Linq;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Linq;
using Couchbase.Linq.Filters;
using Couchbase.N1QL;
using Newtonsoft.Json;

public class Movie : MainDocument {

    public string Name { get; set; }

    public DateTime Release { get; set; }

    public string Description { get; set; }

    public List<string> Actors = new List<string>();

    public static readonly CouchFacade Facade = CouchFacade.create("movies");

    public Movie(string id, string name) : base(id)
    {
        this.Name = name;
    }

    public Movie(string id, string name, List<string> actors) : this(id, name)
    {
        this.Actors = actors;
    }

    [JsonConstructor]
    public Movie(string id, string name, List<string> actors, DateTime release, string description) : this(id, name, actors)
    {
        this.Release = release;
        this.Description = description;
    }

    public List<Actor> GetActors()
    {
        List<Actor> actors = new List<Actor>();

        Actors.ForEach(id => actors.Add(Actor.Facade.GetDocument<Actor>(id)));
        return actors;
    }

    public static List<Movie> Where(string value, int count = 10)
    {
        var context = new BucketContext(ClusterHelper.GetBucket("movies"));
        var query = (from m in context.Query<Movie>()
                     where m.Name.Contains(value)
                     select m).Take(count);

        return query.ToList();
    }
}
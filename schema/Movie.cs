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
    
    private static readonly string _table = "movies";

    public static readonly CouchFacade Facade = CouchFacade.create(_table);

    public static Movie Empty
    {
        get => new Movie(String.Empty, "No Result!");
    }

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

    public static Movie ByID(string id)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Movie>()
                     where m.ID == id
                     select m);

        return query.Count() == 0 ? Movie.Empty : query.First();
    }

    public static Movie WhereName(string name)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Movie>()
                     where m.Name == name
                     select m);

        return query.Count() == 0 ? Movie.Empty : query.First();
    }

    public static List<Movie> WhereNameLike(string name, int count = 10)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Movie>()
                     where m.Name.Contains(name)
                     select m).Take(count);

        return query.ToList();
    }

    public static void Print(Movie movie)
    {
        Console.WriteLine(String.Format("Name: {0}", movie.Name));
        Console.WriteLine(String.Format("Description: {0}", movie.Description));
        Console.WriteLine(String.Format("Release: {0}", movie.Release));
        Console.WriteLine("Actors: ");
        movie.GetActors().ForEach(m => 
        {
            Console.WriteLine(String.Format("Name: {0}", m.Name));
        });

        Console.WriteLine(String.Empty);
    }
}
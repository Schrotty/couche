using System;
using System.Linq;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Linq;
using Couchbase.Linq.Filters;
using Couchbase.N1QL;
using Newtonsoft.Json;

/// <summary>
/// Class representing an actor.
/// </summary>
public class Actor : MainDocument {

    /// <summary>
    /// Name of the actor.
    /// </summary>
    /// <returns>The name</returns>
    public string Name { get; set; }

    /// <summary>
    /// The used facade for this class.
    /// </summary>
    /// <returns>The facade</returns>
    public static readonly CouchFacade Facade = CouchFacade.create("actors");

    /// <summary>
    /// The name of the used bucket.
    /// </summary>
    private static readonly string _table = "actors";

    /// <summary>
    /// Create an empty actor.
    /// </summary>
    /// <returns>An empty actor</returns>
    public static Actor Empty
    {
        get => new Actor(String.Empty, "No Result!");
    }

    /// <summary>
    /// Create a new actor.
    /// </summary>
    /// <param name="id">Actors id</param>
    /// <param name="name">Actors name</param>
    /// <returns>A new actor</returns>
    public Actor(string id, string name) : base(id)
    {
        this.Name = name;
    }

    /// <summary>
    /// Get an actor by it's id.
    /// </summary>
    /// <param name="id">The id to look for</param>
    /// <returns>An empty actor or the found actor</returns>
    public static Actor ByID(string id)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Actor>()
                     where m.ID == id
                     select m);

        return query.Count() == 0 ? Actor.Empty : query.First();
    }

    /// <summary>
    /// Get an actor by it's name.
    /// </summary>
    /// <param name="name">The name to search for</param>
    /// <returns>An empty actor or the found actor</returns>
    public static Actor WhereName(string name)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Actor>()
                     where m.Name == name
                     select m);

        return query.Count() == 0 ? Actor.Empty : query.First();
    }

    /// <summary>
    /// Get a list of actors by a string.
    /// </summary>
    /// <param name="name">The string to search for</param>
    /// <param name="count">The count of actors to return</param>
    /// <returns>A list with actors</returns>
    public static List<Actor> WhereNameLike(string name, int count = 10)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Actor>()
                     where m.Name.Contains(name)
                     select m).Take(count);

        return query.ToList();
    }

    /// <summary>
    /// Print an actor on console.
    /// </summary>
    /// <param name="actor">The actor to print</param>
    public static void Print(Actor actor)
    {
        Console.WriteLine(String.Format("Name: {0}", actor.Name));
        Console.WriteLine(String.Empty);
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Linq;
using Couchbase.Linq.Filters;
using Couchbase.N1QL;
using Newtonsoft.Json;

public class Actor : MainDocument {

    public string Name { get; set; }

    public static readonly CouchFacade Facade = CouchFacade.create("actors");

    private static readonly string _table = "actors";

    public static Actor Empty
    {
        get => new Actor(String.Empty, "No Result!");
    }

    public Actor(string id, string name) : base(id)
    {
        this.Name = name;
    }

    public static Actor ByID(string id)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Actor>()
                     where m.ID == id
                     select m);

        return query.Count() == 0 ? Actor.Empty : query.First();
    }

    public static Actor WhereName(string name)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Actor>()
                     where m.Name == name
                     select m);

        return query.Count() == 0 ? Actor.Empty : query.First();
    }

    public static List<Actor> WhereNameLike(string name, int count = 10)
    {
        var context = new BucketContext(ClusterHelper.GetBucket(_table));
        var query = (from m in context.Query<Actor>()
                     where m.Name.Contains(name)
                     select m).Take(count);

        return query.ToList();
    }

    public static void Print(Actor actor)
    {
        Console.WriteLine(String.Format("Name: {0}", actor.Name));
        Console.WriteLine(String.Empty);
    }
}
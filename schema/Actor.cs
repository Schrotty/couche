public class Actor : MainDocument {

    public string Name { get; set; }

    public static readonly CouchFacade Facade = CouchFacade.create("actors");

    public Actor(string id, string name) : base(id)
    {
        this.Name = name;
    }
}
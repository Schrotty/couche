using Couchbase.Core;

public abstract class MainDocument
{
    public string ID {
        get;
        set;
    }

    public MainDocument(string id)
    {
        this.ID = id;
    }
}
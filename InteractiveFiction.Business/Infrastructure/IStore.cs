namespace InteractiveFiction.Business.Infrastructure
{
    public interface IStore<Key, Obj>
    {
        List<Key> Keys { get; }
        Obj Load(Key key);
        void Save(Key key, Obj value);
    }
}
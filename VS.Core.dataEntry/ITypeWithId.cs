namespace VS.Core.dataEntry
{
    public interface ITypeWithId
    {
        public string Id { get; set; }
        public bool? Deleted { get; set; }
    }
}

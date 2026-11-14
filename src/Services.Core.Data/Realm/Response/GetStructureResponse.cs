namespace Aether.Services.Core.Data.Realm.Response
{
    public class GetStructureResponse
    {
        public Guid? RealmId { get; init; }
        public ResponseStatus Status { get; init; }
        public string? Name { get; init; }
        public string? Icon { get; init; }
        public ICollection<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
    }
}

namespace Aether.Data.Repositories.Contracts.Common
{
    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}

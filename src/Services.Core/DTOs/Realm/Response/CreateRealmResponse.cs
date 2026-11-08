using Aether.Services.Core.DTOs.Realm.Enums;

namespace Aether.Services.Core.DTOs.Realm.Response
{
    public class CreateRealmResponse
    {
        public Guid? RealmId { get; set; } 
        public RealmResponseStatus Status { get; set; }
    }
}

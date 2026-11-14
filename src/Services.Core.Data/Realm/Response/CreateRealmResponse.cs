using Aether.Services.Core.Data;

namespace Aether.Services.Core.Data.Realm.Response
{
    public class CreateRealmResponse
    {
        public Guid? RealmId { get; set; } 
        public ResponseStatus Status { get; set; }
    }
}

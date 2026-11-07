namespace Aether.Services.Core.DTOs.Request
{
    public class GetJoinedRealmsRequest
    {
        public string UserId { get; set; } = null!;

        /// <summary>
        /// Number of Realms you want to retrieve. Defaults to 10.
        /// </summary>
        public int Take { get; set; } = 10;

        /// <summary>
        /// Number of Realms you want to skip. Defaults to 0.
        /// </summary>
        public int Skip { get; set; } = 0;
    }
}

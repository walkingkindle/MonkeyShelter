using Microsoft.Extensions.Logging;

namespace Infrastructure.Implementations
{
    public class DbHelper : IDbHelper
    {
        private readonly ILogger<DbHelper> _logger;

        public DbHelper(ILogger<DbHelper> logger)
        {
            _logger = logger;
        }

        public async Task CarefulSaveChanges(MonkeyShelterDbContext context)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"An exception occurred while saving the changes , {ex.Message} ");
                throw;
            }

        }
    }
}

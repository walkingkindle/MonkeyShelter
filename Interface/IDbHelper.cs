namespace Infrastructure
{
    public interface IDbHelper
    {
        public Task CarefulSaveChanges(MonkeyShelterDbContext dbContext);
    }
}

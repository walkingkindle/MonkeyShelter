namespace Infrastructure.Contracts
{
    public interface IDeparturesRepository
    {
        public int GetTodayDeparturesAmount();

        public Task Depart(int monkeyId);
    }
}

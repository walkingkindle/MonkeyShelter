namespace Application.Contracts.Repositories
{
    public interface IDeparturesRepository
    {
        public int GetTodayDeparturesAmount();

        public Task Depart(int monkeyId);
    }
}

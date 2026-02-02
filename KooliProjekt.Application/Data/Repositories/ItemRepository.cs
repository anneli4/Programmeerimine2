namespace KooliProjekt.Application.Data.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

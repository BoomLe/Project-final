using Microsoft.EntityFrameworkCore;
using MVCT.Data;
using MVCT.IRepository;
using MVCT.Models.Attandance;

namespace MVCT.Repository.ICheckinRepository
{
    public class AttandancesRepository : Repository<Attandances>, IAttandancesRepository
    {
        private readonly ApplicationDbContext _db;
        public AttandancesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Attandances> UpdatedAsync(Attandances attandances)
        {
            // Set the state of the entity to Modified
            _db.Entry(attandances).State = EntityState.Modified;

            // Save the changes to the database
            await _db.SaveChangesAsync();

            return attandances;
        }


    }
}

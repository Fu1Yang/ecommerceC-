using ecommerce.Data;
using ecommerce.DTO;
using ecommerce.Models;

namespace ecommerce.Services
{
    public class RdvService
    {
        private readonly DataContext _db;

        public RdvService(DataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(RdvDto dto)
        {
            var rdv = new Rdv
            {
                Name = dto.Name,
                Email = dto.Email,
                Registrations = dto.Registrations,
                Comment = dto.Comment,

            };

            try
            {
                _db.Rdvs.Add(rdv);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return;
            }

        }
    }
}

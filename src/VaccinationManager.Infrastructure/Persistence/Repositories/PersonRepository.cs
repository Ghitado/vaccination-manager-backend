using Microsoft.EntityFrameworkCore;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
	private readonly VaccinationManagerDbContext _context;

	public PersonRepository(VaccinationManagerDbContext context) => _context = context;

	public async Task<PaginatedResult<Person>> GetPaginated(int pageNumber = 1, int pageSize = 10)
	{
		var total = await _context.Persons.CountAsync();

		var items = await _context.Persons
			.AsNoTracking()
			.OrderBy(p => p.Name)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return new PaginatedResult<Person>(items, total, pageNumber, pageSize);
	}

	public async Task<Person?> GetById(Guid id) =>
		await _context.Persons
			.AsNoTracking()
			.Include(p => p.VaccinationRecords)
				.ThenInclude(v => v.Vaccine)
			.FirstOrDefaultAsync(p => p.Id == id);

	public async Task<Person> Add(Person person)
	{
		_context.Persons.Add(person);
		await _context.SaveChangesAsync();
		return person;
	}

	public async Task Delete(Person person)
	{
		_context.Persons.Remove(person);
		await _context.SaveChangesAsync();
	}
}


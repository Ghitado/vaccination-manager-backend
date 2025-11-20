using Microsoft.EntityFrameworkCore;
using System.Linq;
using VaccinationManager.Domain.Common;
using VaccinationManager.Domain.Entities;
using VaccinationManager.Domain.Repositories;

namespace VaccinationManager.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
	private readonly VaccinationManagerDbContext _context;

	public PersonRepository(VaccinationManagerDbContext context) => _context = context;

	public async Task<PaginatedResult<Person>> GetAll(int? pageNumber, int? pageSize)
	{
		var pageNumberDefault = pageNumber ?? 1;
		var pageSizeDefault = pageSize ?? 10;

		var total = await _context.Persons.CountAsync();
		var items = await _context.Persons
			.Include(p => p.VaccinationRecords)
			.OrderBy(p => p.Name)
			.Skip((pageNumberDefault - 1) * pageSizeDefault)
			.Take(pageSizeDefault)
			.ToListAsync();

		return new PaginatedResult<Person>(items, total, pageNumberDefault, pageSizeDefault);
	}

	public async Task<Person?> GetById(Guid id) =>
		await _context.Persons
			.Include(p => p.VaccinationRecords)
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


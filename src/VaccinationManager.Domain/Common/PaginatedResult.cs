namespace VaccinationManager.Domain.Common;

public sealed class PaginatedResult<T>
{
	public List<T> Items { get; init; } = [];
	public int TotalCount { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; } 

	public PaginatedResult(List<T> items, int totalCount, int pageNumber, int pageSize)
	{
		Items = items ?? new List<T>();
		TotalCount = totalCount;
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}

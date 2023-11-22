namespace Application.Common.Helpers.Pagination;

public record PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    protected PaginationRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
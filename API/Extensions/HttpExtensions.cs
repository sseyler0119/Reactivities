using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int 
            itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new //anonymous object
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader)); // format paginationHeader as json string with Pagination key
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination"); // must use "Access-Control-Expose-Headers" exactly
        }
    }
}
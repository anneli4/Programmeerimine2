using System.Net.Http.Json;
using System.Text.Json;

namespace Kooli.ProjektForm;

internal sealed class ClientApiService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public ClientApiService(string baseAddress)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
    }

    public async Task<List<T>> GetListAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        var items = await _httpClient.GetFromJsonAsync<List<T>>(endpoint, JsonOptions, cancellationToken);
        return items ?? new List<T>();
    }

    public async Task<int> CreateAsync<TRequest>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync(endpoint, request, JsonOptions, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);

        var createdId = await response.Content.ReadFromJsonAsync<int>(JsonOptions, cancellationToken);
        return createdId;
    }

    public async Task DeleteClientAsync(int id, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.DeleteAsync($"api/Clients/{id}", cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var details = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(details))
        {
            response.EnsureSuccessStatusCode();
            return;
        }

        throw new HttpRequestException($"API request failed ({(int)response.StatusCode}): {details}");
    }
}

internal sealed class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Discount { get; set; }
}

internal sealed class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

internal sealed class ItemDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Photo { get; set; } = string.Empty;
}

internal sealed class OrderDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ClientId { get; set; }
    public decimal Discount { get; set; }
}

internal sealed class InvoiceDto
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal Paid { get; set; }
}

internal sealed class CreateClientRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public decimal Discount { get; init; }
}

internal sealed class CreateCategoryRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

internal sealed class CreateItemRequest
{
    public int CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public string Photo { get; init; } = string.Empty;
}

internal sealed class CreateOrderRequest
{
    public DateTime Date { get; init; }
    public int ClientId { get; init; }
    public decimal Discount { get; init; }
}

internal sealed class CreateInvoiceRequest
{
    public string InvoiceNumber { get; init; } = string.Empty;
    public int OrderId { get; init; }
    public int ClientId { get; init; }
    public DateTime Date { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal Discount { get; init; }
    public decimal Paid { get; init; }
}
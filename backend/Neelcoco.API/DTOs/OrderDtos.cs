namespace Neelcoco.API.DTOs;

public record OrderItemRequest(int ProductId, int Quantity);

public record CreateOrderRequest(
    string CustomerName,
    string Email,
    string Phone,
    string Address,
    string City,
    List<OrderItemRequest> Items
);

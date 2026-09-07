namespace Neelcoco.API.DTOs;

public record ContactRequest(
    string Name,
    string? Email,
    string? Phone,
    string Message
);

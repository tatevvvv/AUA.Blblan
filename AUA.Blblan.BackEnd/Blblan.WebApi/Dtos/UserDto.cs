namespace Blblan.Common.Models;

public record UserDto(string Fullname, float Balance);

public record UserEntityDto(string FirstName, string LastName, float Balance, int Id);
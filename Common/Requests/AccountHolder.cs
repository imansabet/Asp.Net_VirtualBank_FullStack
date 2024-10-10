namespace Common.Requests;

public record CreateAccountHolder
    (
    string FirstName
    ,string LastName
    ,DateTime DateOfBirth
    ,string Email
    ,string ContactNumber
    );

public record UpdateAccountHolder
    (
    int Id
    , string FirstName
    , string LastName
    , string Email
    , string ContactNumber
    );


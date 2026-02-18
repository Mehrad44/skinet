using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public string firstName {get; set;} = string.Empty;

    [Required]
    public string LastName {get; set;} = string.Empty;


    [Required]
    public string Email {get; set;} = string.Empty;


    [Required]
    public string Password {get; set;} = string.Empty;



}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataContext.Models;

public partial class KritiUser
{
    public int UserId { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(50)]
    public string? MiddleName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    [Required]
    [MaxLength(10)]
    public string Gender { get; set; } = null!;
    [Required]
    public DateOnly DateOfJoining { get; set; }
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = null!;
    [Required]
    [Phone]
    [MaxLength(20)]
    public string Password { get; set; } = null!;
    [Required]
    [Phone]
    [MaxLength(15)]
    public string Phone { get; set; } = null!;
    [Phone]
    [MaxLength(15)]
    public string? AlternatePhone { get; set; }
    [MaxLength(255)]
    public string? ImgPath { get; set; }
    [Required]
    [MaxLength(255)]
    public string PrimaryAddress { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string PrimaryCity { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string PrimaryState { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string PrimaryCountry { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string PrimaryZipCode { get; set; } = null!;
    [MaxLength(255)]
    public string? SecondaryAddress { get; set; }
    [MaxLength(100)]
    public string? SecondaryCity { get; set; }
    [MaxLength(100)]
    public string? SecondaryState { get; set; }
    [MaxLength(100)]
    public string? SecondaryCountry { get; set; }
    [MaxLength(20)]
    public string? SecondaryZipCode { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ResetPasswordToken { get; set; }

    public DateTime? ResetExpiryToken { get; set; }
}

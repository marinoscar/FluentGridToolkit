using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FluentGridToolkit.Sample.Model
{
    /// <summary>
    /// Represents an Account entity used to store information about a company's account details.
    /// </summary>
    [Table("Account")]
    public class Account
    {
        /// <summary>
        /// Primary key for the Account entity.
        /// </summary>
        [Key]
        [Column("Id")]
        public ulong Id { get; set; }

        /// <summary>
        /// The name of the company being served.
        /// </summary>
        [Required(ErrorMessage = "The company name is required.")]
        [MinLength(2, ErrorMessage = "The company name must be at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "The company name cannot exceed 100 characters.")]
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// The email address for the company.
        /// </summary>
        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email must be a valid email address.")]
        [Column("Email")]
        public string Email { get; set; }

        /// <summary>
        /// The total sales for the year for the company.
        /// </summary>
        [Required(ErrorMessage = "Total sales is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total sales must be a positive number.")]
        [Column("TotalSales")]
        public double? TotalSales { get; set; }

        /// <summary>
        /// The state where the company is located.
        /// </summary>
        [Required(ErrorMessage = "State is required.")]
        [MaxLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        [Column("State")]
        public string State { get; set; }

        /// <summary>
        /// The country where the company is located.
        /// </summary>
        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        [Column("Country")]
        public string Country { get; set; }

        /// <summary>
        /// The date the account was created.
        /// </summary>
        [Required(ErrorMessage = "The account creation date is required.")]
        [Column("AccountCreatedDate")]
        public DateTime AccountCreatedDate { get; set; }

        /// <summary>
        /// The UTC date and time the record was created.
        /// </summary>
        [Column("UtcCreatedOn")]
        public DateTime UtcCreatedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The user who created the record.
        /// </summary>
        [Column("CreatedBy")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// The UTC date and time the record was last updated.
        /// </summary>
        [Column("UtcUpdatedOn")]
        public DateTime? UtcUpdatedOn { get; set; }

        /// <summary>
        /// The user who last updated the record.
        /// </summary>
        [Column("UpdatedBy")]
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// The version of the record, incremented on each update.
        /// </summary>
        [Column("Version")]
        public uint Version { get; set; }

        /// <summary>
        /// Provides a string representation of the Account entity.
        /// </summary>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
        }
    }

}

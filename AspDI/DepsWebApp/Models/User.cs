using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Markup;

namespace DepsWebApp
{
    /// <summary>
    /// User Class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of user
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  private set; }
        /// <summary>
        /// User Login
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Login { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
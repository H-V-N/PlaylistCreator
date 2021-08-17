using System.ComponentModel.DataAnnotations;

namespace SpotifyCache.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
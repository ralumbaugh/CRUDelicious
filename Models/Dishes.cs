using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get; set;}
        [Required (ErrorMessage = "Your dish requires a name")]
        [MaxLength(45, ErrorMessage = "Whoah there, that's a really long dish name. Try something 45 characters or less.")]
        public string Name {get; set;}
        [Required (ErrorMessage = "Someone's gotta make the dish. Who's the chef?")]
        [MaxLength(45, ErrorMessage = "I want name, not a dissertation. Keep it 45 characters or less.")]
        public string Chef {get; set;}
        [Required (ErrorMessage = "Give it a rating!")]
        [Range(1,5, ErrorMessage = "Your rating should be between 1 and 5 stars.")]
        public int? Tastiness {get; set;}
        [Required (ErrorMessage = "It's not diet cheat day! Your dish must have calories!")]
        [Range (0, double.PositiveInfinity, ErrorMessage="Your food must have a positive amount of calories.")]
        public int? Calories {get; set;}
        [Required (ErrorMessage = "Please input a description of the food. Otherwise no one will know what it's like!")]
        public string Description {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}
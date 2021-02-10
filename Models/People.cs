using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Moment2.Models
{
    public class People
    {
        [Required(ErrorMessage ="Du måste fylla i ett namn")]
        [MinLength(2, ErrorMessage = "Namnet måste vara minst 2 tecken.")]
        [DisplayName("Namn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Du måste ange ett datum")]
        [DataType(DataType.Date)]
        [DisplayName("Födelsedatum")]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage ="Du måste välja färg")]
        [DisplayName("Välj en färg")]
        public Color FavoriteColor { get; set; }
        [Required(ErrorMessage ="Du måste önska dig något")]
        [DisplayName("Vad önskar du dig?")]
        public string gift { get; set; }
        
        public int Age { get; set; }
        public int DaysToCake { get; set; }
        [DataType(DataType.Date)]

        public void CountDaysToBirthday()
        {
            DateTime TodayDate = DateTime.Now;

            DateTime next = new DateTime(TodayDate.Year, Birthdate.Month, Birthdate.Day);

            if (next < TodayDate)
                next = next.AddYears(1);

            DaysToCake = (next - TodayDate).Days;

        }

        public void GetAge()
        {
            DateTime TodayDate = DateTime.Now;

            // Calculate the age.
            Age = TodayDate.Year - Birthdate.Year + 1;

            // Go back to the year in which the person was born in case of a leap year
            if (Birthdate.Date > TodayDate.AddYears(-Age)) Age--;

        }

        public enum Color
        {
            rosa,
            turkos, 
            orange,
            gul,
            lila
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackPro.Models
{
    // Abstract base class for polymorphism demonstration
    public abstract class NutritionItem
    {
        // Abstract method that derived classes must implement
        public abstract decimal calculateNutritionalValue(string macroType);

        // Virtual method that can be overridden
        public virtual string getDisplayName()
        {
            return "Nutrition Item";
        }

        // Shared property
        public DateTime createdDate { get; set; } = DateTime.UtcNow;
    }
}

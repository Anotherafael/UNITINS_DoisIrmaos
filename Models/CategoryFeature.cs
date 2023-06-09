﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNITINS_DoisIrmaos.Models
{
    public class CategoryFeature
    {
        public int CategoryID { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }

        public int FeatureID { get; set; }

        [Display(Name = "Feature")]
        public Feature Feature { get; set; }
        public CategoryFeature()
        {

        }
        public CategoryFeature(int categoryID, int featureID)
        {
            this.CategoryID = categoryID;
            this.FeatureID = featureID;
        }

    }
}

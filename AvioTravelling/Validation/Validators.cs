using AvioTravelling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Validators;
using FluentValidation;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace AvioTravelling.Validation
{
    public partial class Validators
    {
        public class CountryValidator : AbstractValidator<Country>
        {
            public CountryValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class CityValidator : AbstractValidator<City>
        {
            public CityValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.PictureLink).NotEmpty();
                RuleFor(x => x.PricePerDay).NotEmpty();
                RuleFor(x => x.Country).NotEmpty();
            }
        }


        public class ShowplaceValidator : AbstractValidator<Showplace>
        {
            public ShowplaceValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                //RuleFor(x => x.PictureLink).NotEmpty();
                RuleFor(x => x.LongDescription).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }
    }
}
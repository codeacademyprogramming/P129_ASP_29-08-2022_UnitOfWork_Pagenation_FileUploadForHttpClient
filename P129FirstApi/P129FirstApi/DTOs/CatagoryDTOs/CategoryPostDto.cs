﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P129FirstApi.DTOs.CatagoryDTOs
{
    public class CategoryPostDto
    {
        public string Ad { get; set; }
        public IFormFile Sekli { get; set; }
        public bool Esasdirmi { get; set; }
        public Nullable<int> AidOlduguUstCategoryId { get; set; }
    }

    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Ad)
                .MaximumLength(255).WithMessage("Ad Maksimum 255 Simvol Ola Biler")
                .MinimumLength(10).WithMessage("Ad Minimum 10 Simvol Ola Biler")
                .NotEmpty().WithMessage("Mecburidi Qaqa");

            RuleFor(x => x.Sekli.FileName)
                .MaximumLength(1000).WithMessage("Sekli Maksimum 1000 Simvol Ola Biler")
                .MinimumLength(5).WithMessage("Ad Minimum 5 Simvol Ola Biler");

            RuleFor(x => x).Custom((x,context) =>
                {
                    if (x.Esasdirmi)
                    {
                        if (x.Sekli == null)
                        {
                            context.AddFailure("Sekil Mutleqdir");
                        }
                    }
                    else
                    {
                        if (x.AidOlduguUstCategoryId == null)
                        {
                            context.AddFailure("AidOlduguUstCategoryId Mutleqdir");
                        }
                    }
                });
        }
    }
}

﻿using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Service.Validators
{
    public class RendaFamiliarValidator : AbstractValidator<RendaFamiliar>
    {
        public RendaFamiliarValidator()
        {

        }
    }
}

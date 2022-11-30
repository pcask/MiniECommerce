﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle
{
    public class LoginWithGoogleCommandRequest : IRequest<LoginWithGoogleCommandResponse>
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
}

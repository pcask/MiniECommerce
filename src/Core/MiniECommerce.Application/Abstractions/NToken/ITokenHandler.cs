using MiniECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.NToken
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}

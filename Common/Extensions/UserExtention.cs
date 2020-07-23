using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Common.Extensions
{
    public static class UserExtention
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public static string GetUserMail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name).Value;
        }
    }
}

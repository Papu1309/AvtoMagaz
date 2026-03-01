using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoMagaz
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static int? RoleId { get; set; }
    }
}

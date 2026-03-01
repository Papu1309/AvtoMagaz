using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoMagaz.Connect
{
    public static class Connection
    {
        // Статический экземпляр контекста для всего приложения
        public static AvtomagazEntities2 entities = new AvtomagazEntities2();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DomainServices.Interfaces.Infraestructure
{
    public interface IMapper
    {
        D Map<D>(object from) where D : new();
    }
}

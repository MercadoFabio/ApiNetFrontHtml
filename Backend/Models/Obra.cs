using System;
using System.Collections.Generic;

namespace Parcial.Models;

public partial class Obra
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string DatosVarios { get; set; } = null!;

    public Guid IdTipoObra { get; set; }

    public virtual ICollection<AlbanilesXObra> AlbanilesXObras { get; set; } = new List<AlbanilesXObra>();

    public virtual TiposObra IdTipoObraNavigation { get; set; } = null!;
}

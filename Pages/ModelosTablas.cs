public class Prestamos
{
    public DateTime fecha_prestamo { get; set; }
    public DateTime fecha_entrega { get; set; }
    public string? estado { get; set; }
    public string? Cliente { get; set; }
    public string? Libro { get; set; }
    
}

public class Autores
{
    public string? id { get; set; }
    public string? nombre { get; set; }
    public string? apellido { get; set; }
    public string? pais { get; set; }
    public string? descripcion { get; set; }
}

public class Categorias
{
    public string? id { get; set; }
    public string? nombre { get; set; }

}

public class Clientes
{
    public string? id { get; set; }
    public string? nombre { get; set; }
    public string? apellido { get; set; }
    public string? correo { get; set; }
    public int? matricula { get; set; }
    public string? foto_perfil { get; set; }

}

public class Editorial
{
    public string? id { get; set; }
    public string? nombre { get; set; }
}

public class Libros
{
    
    public string? id { get; set; }
    public string? nombre { get; set; }
    public int paginas { get; set; }
    public string? descripcion { get; set; }
    public int edicion { get; set; }
    public DateTime fecha_publicacion { get; set; }
    public string? Autor { get; set; }
    public string? autor_id { get; set; }
    public string? categoria { get; set; }
    public string? categoria_id { get; set; }
    public string? editorial { get; set; }
    public string? editorial_id { get; set; }
}

public class Usuario
{
    public string? nombre { get; set; }
    public string? apellido { get; set; }
}

public class Reportes
{
    public string? id { get; set; }
    public DateTime fecha_prestamo { get; set; }
    public string? Cliente { get; set; }
    public string? cliente_id { get; set; }
}
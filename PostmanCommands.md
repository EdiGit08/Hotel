http://localhost:5276/Token/Autenticar
{
    "Usuario" : "[User dado por el sistema]"
}

http://localhost:5276/[Entida]/Listar
{
"Bearer" : "[Token User dado por el sistema]"
}

http://localhost:5276/[Entida]/Guardar
{
    "Bearer" : "[Token User dado por el sistema]",
    'Entidad' : {[Entidad a crear sin id]}
}

http://localhost:5276/[Entida]/PorCodigo
{
    "Bearer" : "[Token User dado por el sistema]",
    'Entidad' : {"Codigo" : "[Codigo a buscar]"}
}

http://localhost:5276/[Entida]/Modificar
{
    "Bearer" : "[Token User dado por el sistema]",
    'Entidad' : {[Toda la entidad a modificar con id]}
} 

http://localhost:5276/Clientes/Borrar
{
    "Bearer" : "[Token User dado por el sistema]"
    'Entidad' : {"id" : [id a borrar]}
}

# EJEMPLO ----------------------------------------------------------------------------------------

http://localhost:5276/Token/Autenticar
{
    "Usuario" : "SBWq+oLWYfEUF42esqPTIw=="
}

http://localhost:5276/Clientes/Listar
{
"Bearer" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
            eyJ1bmlxdWVfbmFtZSI6IlNCV3Erb0xXWWZFVUY0MmVzcVBUSXc
            9PSIsIm5iZiI6MTc0NzA2MzUwOSwiZXhwIjoxNzQ3MDY3MTA5LC
            JpYXQiOjE3NDcwNjM1MDl9.DfkP3uOFbXOfp1zSjjRDntHzRTGOC_YONvUfc-X2NH8"
}

http://localhost:5276/Clientes/Guardar
{
    "Bearer" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
            eyJ1bmlxdWVfbmFtZSI6IlNCV3Erb0xXWWZFVUY0MmVzcVBUSXc
            9PSIsIm5iZiI6MTc0NzA2MzUwOSwiZXhwIjoxNzQ3MDY3MTA5LC
            JpYXQiOjE3NDcwNjM1MDl9.DfkP3uOFbXOfp1zSjjRDntHzRTGOC_YONvUfc-X2NH8",
    'Entidad' : {"Cedula":"1033487061","Nombre":"Edison Osorio","Opinion":5,"_Opinion":null}
}

http://localhost:5276/Clientes/PorCodigo
{
    "Bearer" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
            eyJ1bmlxdWVfbmFtZSI6IlNCV3Erb0xXWWZFVUY0MmVzcVBUSXc
            9PSIsIm5iZiI6MTc0NzA2MzUwOSwiZXhwIjoxNzQ3MDY3MTA5LC
            JpYXQiOjE3NDcwNjM1MDl9.DfkP3uOFbXOfp1zSjjRDntHzRTGOC_YONvUfc-X2NH8",
    'Entidad' : {"Cedula":"1033487061"}
}

http://localhost:5276/Clientes/Modificar
{
    "Bearer" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
            eyJ1bmlxdWVfbmFtZSI6IlNCV3Erb0xXWWZFVUY0MmVzcVBUSXc
            9PSIsIm5iZiI6MTc0NzA2MzUwOSwiZXhwIjoxNzQ3MDY3MTA5LC
            JpYXQiOjE3NDcwNjM1MDl9.DfkP3uOFbXOfp1zSjjRDntHzRTGOC_YONvUfc-X2NH8",
    'Entidad' : {"id":11, "Cedula":"1033487061","Nombre":"Edison Osorio Botero","Opinion":5,"_Opinion":null}
} 

http://localhost:5276/Clientes/Borrar
{
    "Bearer" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
            eyJ1bmlxdWVfbmFtZSI6IlNCV3Erb0xXWWZFVUY0MmVzcVBUSXc
            9PSIsIm5iZiI6MTc0NzA2MzUwOSwiZXhwIjoxNzQ3MDY3MTA5LC
            JpYXQiOjE3NDcwNjM1MDl9.DfkP3uOFbXOfp1zSjjRDntHzRTGOC_YONvUfc-X2NH8",
    'Entidad' : {"id" : 11}
}

using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static List<Empleado> listaEmpleados = new List<Empleado>();

    static void Main(string[] args)
    {
        CargarDatosDesdeArchivo();

        int opcion;
        do
        {
            MostrarMenu();
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    AgregarEmpleado();
                    break;
                case 2:
                    ConsultarEmpleado();
                    break;
                case 3:
                    ModificarEmpleado();
                    break;
                case 4:
                    EliminarEmpleado();
                    break;
                case 5:
                    GuardarEnArchivo();
                    break;
                case 6:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }
        } while (opcion != 6);
    }

    static void MostrarMenu()
    {
        Console.WriteLine("Menú de Empleados:");
        Console.WriteLine("1- Agregar Empleado");
        Console.WriteLine("2- Consultar Empleado");
        Console.WriteLine("3- Modificar Empleado");
        Console.WriteLine("4- Eliminar Empleado");
        Console.WriteLine("5- Guardar en Archivo");
        Console.WriteLine("6- Salir");
        Console.Write("Seleccione una opción: ");
    }

    static void CargarDatosDesdeArchivo()
    {
        try
        {
            // Leer datos desde el archivo y cargarlos en la listaEmpleados
            if (File.Exists("empleados.txt"))
            {
                using (StreamReader sr = new StreamReader("empleados.txt"))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');
                        Empleado empleado = new Empleado
                        {
                            Id_Empleado = int.Parse(datos[0]),
                            Nombre = datos[1],
                            Direccion = datos[2],
                            Telefono = datos[3],
                            Edad = int.Parse(datos[4]),
                            Salario = decimal.Parse(datos[5])
                        };
                        listaEmpleados.Add(empleado);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar datos desde el archivo: " + ex.Message);
        }
    }

    static void AgregarEmpleado()
    {
        Console.WriteLine("Ingrese los datos del nuevo empleado:");

        // Solicitar datos al usuario
        Console.Write("Id del Empleado: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Dirección: ");
        string direccion = Console.ReadLine();

        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine();

        Console.Write("Edad: ");
        int edad = int.Parse(Console.ReadLine());

        Console.Write("Salario: ");
        decimal salario = decimal.Parse(Console.ReadLine());

        // Crear nuevo objeto Empleado
        Empleado nuevoEmpleado = new Empleado
        {
            Id_Empleado = id,
            Nombre = nombre,
            Direccion = direccion,
            Telefono = telefono,
            Edad = edad,
            Salario = salario
        };

        // Agregar empleado a la lista
        listaEmpleados.Add(nuevoEmpleado);
        Console.WriteLine("Empleado agregado correctamente.");
    }

    static void ConsultarEmpleado()
    {
        Console.Write("Ingrese el Id del empleado a consultar: ");
        int idConsulta = int.Parse(Console.ReadLine());

        // Buscar empleado por Id
        Empleado empleadoEncontrado = listaEmpleados.Find(e => e.Id_Empleado == idConsulta);

        if (empleadoEncontrado != null)
        {
            Console.WriteLine("Datos del empleado:");
            Console.WriteLine($"Id: {empleadoEncontrado.Id_Empleado}");
            Console.WriteLine($"Nombre: {empleadoEncontrado.Nombre}");
            Console.WriteLine($"Dirección: {empleadoEncontrado.Direccion}");
            Console.WriteLine($"Teléfono: {empleadoEncontrado.Telefono}");
            Console.WriteLine($"Edad: {empleadoEncontrado.Edad}");
            Console.WriteLine($"Salario: {empleadoEncontrado.Salario}");
        }
        else
        {
            Console.WriteLine("Empleado no encontrado.");
        }
    }

    static void ModificarEmpleado()
    {
        Console.Write("Ingrese el Id del empleado a modificar: ");
        int idModificar = int.Parse(Console.ReadLine());

        // Buscar empleado por Id
        Empleado empleadoModificar = listaEmpleados.Find(e => e.Id_Empleado == idModificar);

        if (empleadoModificar != null)
        {
            Console.WriteLine("Ingrese los nuevos datos del empleado:");

            // Solicitar nuevos datos al usuario
            Console.Write("Nombre: ");
            empleadoModificar.Nombre = Console.ReadLine();

            Console.Write("Dirección: ");
            empleadoModificar.Direccion = Console.ReadLine();

            Console.Write("Teléfono: ");
            empleadoModificar.Telefono = Console.ReadLine();

            Console.Write("Edad: ");
            empleadoModificar.Edad = int.Parse(Console.ReadLine());

            Console.Write("Salario: ");
            empleadoModificar.Salario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Empleado modificado correctamente.");
        }
        else
        {
            Console.WriteLine("Empleado no encontrado.");
        }
    }

    static void EliminarEmpleado()
    {
        Console.Write("Ingrese el Id del empleado a eliminar: ");
        int idEliminar = int.Parse(Console.ReadLine());

        // Buscar empleado por Id
        Empleado empleadoEliminar = listaEmpleados.Find(e => e.Id_Empleado == idEliminar);

        if (empleadoEliminar != null)
        {
            // Confirmar eliminación
            Console.WriteLine($"¿Seguro que desea eliminar al empleado {empleadoEliminar.Nombre}? (S/N)");
            string confirmacion = Console.ReadLine();

            if (confirmacion.ToUpper() == "S")
            {
                // Eliminar empleado de la lista
                listaEmpleados.Remove(empleadoEliminar);
                Console.WriteLine("Empleado eliminado correctamente.");
            }
        }
        else
        {
            Console.WriteLine("Empleado no encontrado.");
        }
    }

    static void GuardarEnArchivo()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("empleados.txt"))
            {
                foreach (var empleado in listaEmpleados)
                {
                    sw.WriteLine($"{empleado.Id_Empleado},{empleado.Nombre},{empleado.Direccion},{empleado.Telefono},{empleado.Edad},{empleado.Salario}");
                }
            }
            Console.WriteLine("Datos guardados en archivo correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar datos en el archivo: " + ex.Message);
        }
    }
}

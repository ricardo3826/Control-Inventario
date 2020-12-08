using Inventario.BIZ;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using Inventario.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventario.GUI.Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }
        IManejadorDeEmpleados manejadorEmpleados;

        accion accionEmpleados;
        public MainWindow()
        {
            InitializeComponent();

            manejadorEmpleados = new ManejadorDeEmpleados(new RepositorioDeEmpleados());

            BotonesEmpleadosEnEdicion(false);
            LimpiarCamposEmpleados();
            ActualizarTablaEmpleados();
        }

        private void ActualizarTablaEmpleados()
        {
            dtgEmpleados.ItemsSource=null;
            dtgEmpleados.ItemsSource = manejadorEmpleados.Listar;
        }

        private void LimpiarCamposEmpleados()
        {
            TxboxEmpleadosApellidos.Clear();
            TxboxEmpleadosArea.Clear();
            TxboxEmpleadosNombre.Clear();
            txboxEmpleadoId.Text = "";
        }

        private void BotonesEmpleadosEnEdicion(bool value)
        {
            btnEmpleadoCancelar.IsEnabled = value;
            btnEmpleadoEditar.IsEnabled = !value;
            btnEmpleadoEliminar.IsEnabled = !value;
            btnEmpleadoGuardar.IsEnabled = value;
            btnEmpleadoNuevo.IsEnabled = !value;

        }

        /*private void btnEmpleadoNuevo_Click(object sender, RoutedEventArgs e)
        {
            
        }*/

        private void btnEmpleadoNuevo_Click_1(object sender, RoutedEventArgs e)
        {
            LimpiarCamposEmpleados();
            BotonesEmpleadosEnEdicion(true);
            accionEmpleados = accion.Nuevo;
        }

        private void btnEmpleadoEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if(emp != null)
            {
                txboxEmpleadoId.Text = emp.Id;
                TxboxEmpleadosNombre.Text = emp.Nombre;
                TxboxEmpleadosApellidos.Text = emp.Apellidos;
                TxboxEmpleadosArea.Text = emp.Area;
                accionEmpleados = accion.Editar;
                BotonesEmpleadosEnEdicion(true);
            }
            
        }

        private void btnEmpleadoGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(accionEmpleados == accion.Nuevo)
            {
                Empleado emp = new Empleado()
                {
                    Nombre = TxboxEmpleadosNombre.Text,
                    Apellidos = TxboxEmpleadosApellidos.Text,
                    Area = TxboxEmpleadosArea.Text
                };
                if (manejadorEmpleados.Agregar(emp))
                {
                    MessageBox.Show("Empleado agregado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposEmpleados();
                    ActualizarTablaEmpleados();
                    BotonesEmpleadosEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Empleado no se ha podido agregado", "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Empleado emp = dtgEmpleados.SelectedItem as Empleado;
                emp.Apellidos = TxboxEmpleadosApellidos.Text;
                emp.Nombre = TxboxEmpleadosNombre.Text;
                emp.Area = TxboxEmpleadosArea.Text;
                if (manejadorEmpleados.Modificar(emp))
                {
                    MessageBox.Show("Empleado modificado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposEmpleados();
                    ActualizarTablaEmpleados();
                    BotonesEmpleadosEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Empleado no se ha podido actualizar", "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
}
        

        private void btnEmpleadoCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposEmpleados();
            BotonesEmpleadosEnEdicion(false);
        }

        private void btnEmpleadoEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                if(MessageBox.Show("Realmente deseas eliminar el empleado?"," ", 
                MessageBoxButton.YesNo , MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorEmpleados.Eliminar(emp.Id))
                    {
                        MessageBox.Show("Empleado eliminado","", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaEmpleados();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido eliminar","", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}

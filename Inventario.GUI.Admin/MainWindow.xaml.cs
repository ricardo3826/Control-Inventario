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
        IManejadorDeMateriales manejadorDeMateriales;

        accion accionEmpleados;
        accion accionMateriales;
        public MainWindow()
        {
            InitializeComponent();

            manejadorEmpleados = new ManejadorDeEmpleados(new RepositorioDeEmpleados());
            manejadorDeMateriales = new ManejadorDeMateriales(new RepositorioDeMateriales());

            BotonesEmpleadosEnEdicion(false);
            LimpiarCamposEmpleados();
            ActualizarTablaEmpleados();

            BotonesMaterialesEnEdicion(false);
            LimpiarCamposMateriales();
            ActualizarTablaMateriales();
        }

        private void ActualizarTablaMateriales()
        {
            dtgMateriales.ItemsSource = null;
            dtgMateriales.ItemsSource = manejadorDeMateriales.Listar;
        }

        private void LimpiarCamposMateriales()
        {
            TxboxMaterialNombre.Clear();
            txboxMaterialId.Text = "";
            TxboxMaterialCategoria.Clear();
            TxboxMaterialDescripcion.Clear();
        }

        private void BotonesMaterialesEnEdicion(bool value)
        {
            btnMaterialCancelar.IsEnabled = value;
            btnMaterialEditar.IsEnabled = !value;
            btnMaterialEliminar.IsEnabled = !value;
            btnMaterialGuardar.IsEnabled = value;
            btnMaterialNuevo.IsEnabled = !value;
        }

        private void ActualizarTablaEmpleados()
        {
            dtgEmpleados.ItemsSource = null;
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
            if (emp != null)
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
            if (accionEmpleados == accion.Nuevo)
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
                if (MessageBox.Show("Realmente deseas eliminar el empleado?", " ",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorEmpleados.Eliminar(emp.Id))
                    {
                        MessageBox.Show("Empleado eliminado", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaEmpleados();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido eliminar", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btnMaterialNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposMateriales();
            BotonesMaterialesEnEdicion(true);
            accionMateriales = accion.Nuevo;
        }

        private void btnMaterialEditar_Click(object sender, RoutedEventArgs e)
        {
            Materiales mat = dtgMateriales.SelectedItem as Materiales;
            if (mat != null)
            {
                txboxMaterialId.Text = mat.Id;
                TxboxMaterialNombre.Text = mat.Nombre;
                TxboxMaterialCategoria.Text = mat.Categoria;
                TxboxMaterialDescripcion.Text = mat.Descripcion;
                accionMateriales = accion.Editar;
                BotonesMaterialesEnEdicion(true);
            }
        }

        private void btnMaterialGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionMateriales == accion.Nuevo)
            {
                Materiales mat = new Materiales()
                {
                    Nombre = TxboxMaterialNombre.Text,
                    Categoria = TxboxMaterialCategoria.Text,
                    Descripcion = TxboxMaterialDescripcion.Text
                };
                if (manejadorDeMateriales.Agregar(mat))
                {
                    MessageBox.Show("Material agregado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposMateriales();
                    ActualizarTablaMateriales();
                    BotonesMaterialesEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Material no se ha podido agregado", "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Materiales mat = dtgMateriales.SelectedItem as Materiales;
                mat.Nombre = TxboxMaterialNombre.Text;
                mat.Categoria = TxboxMaterialCategoria.Text;
                mat.Descripcion = TxboxMaterialDescripcion.Text;

                if (manejadorDeMateriales.Modificar(mat))
                {
                    MessageBox.Show("Material modificado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposMateriales();
                    ActualizarTablaMateriales();
                    BotonesMaterialesEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Material no se ha podido actualizar", "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btnMaterialCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposMateriales();
            BotonesMaterialesEnEdicion(false);
        }

        private void btnMaterialEliminar_Click(object sender, RoutedEventArgs e)
        {
            Materiales mat = dtgMateriales.SelectedItem as Materiales;
            if (mat != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar este material?", " ",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorDeMateriales.Eliminar(mat.Id))
                    {
                        MessageBox.Show("Material eliminado", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaMateriales();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido eliminar", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    } 
} 




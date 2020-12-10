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

namespace Inventario.GUI.Almacen
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

        accion accionCheck;

        ManejadorDeCheck manejadorcheck;
        ManejadorDeEmpleados manejadorDeEmpleados;
        ManejadorDeMateriales manejadorDeMateriales;
        Check check;

        
        public MainWindow()
        {
            InitializeComponent();
            manejadorcheck = new ManejadorDeCheck(new RepositorioDeChecks());
            manejadorDeEmpleados = new ManejadorDeEmpleados(new RepositorioDeEmpleados());
            manejadorDeMateriales = new ManejadorDeMateriales(new RepositorioDeMateriales());


            ActualizarTablaCheck();
            GridDetalle.IsEnabled = false;
        }

        private void ActualizarTablaCheck()
        {
            dtgCheck.ItemsSource = null;
            dtgCheck.ItemsSource = manejadorcheck.Listar;
        }

        private void btnNuevoCheck_Click(object sender, RoutedEventArgs e)
        {
            GridDetalle.IsEnabled = true;
            ActualizarComboDetalle();

            check = new Check();
            check.MaterialesPrestados = new List<Materiales>();
            ActualizarListaDeMaterialesEnCheck();

            accionCheck = accion.Nuevo;
        }

        private void ActualizarListaDeMaterialesEnCheck()
        {
            dtgMaterialesEnCheck.ItemsSource = null;
            dtgMaterialesEnCheck.ItemsSource = check.MaterialesPrestados;
        }

        private void ActualizarComboDetalle()
        {
            cmbAlmacenista.ItemsSource = null;
            cmbAlmacenista.ItemsSource = manejadorDeEmpleados.EmpleadosPorArea("Almacen");

            cmbMateriales.ItemsSource = null;
            cmbMateriales.ItemsSource = manejadorDeMateriales.Listar;

            cmbSolicitante.ItemsSource = null;
            cmbSolicitante.ItemsSource = manejadorDeEmpleados.Listar;
        }

        private void btnEliminarCheck_Click(object sender, RoutedEventArgs e)
        {
            Check v = dtgCheck.SelectedItem as Check;
            if (v != null)
            {
                if(MessageBox.Show("Realmente deseas eliminar el check","", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                {
                    if (manejadorcheck.Eliminar(v.Id))
                    {
                        MessageBox.Show("Eliminado con éxito","", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaCheck();
                    }
                    else {
                        MessageBox.Show("Ocurrio un error al eliminar...", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnSumar_Click(object sender, RoutedEventArgs e)
        {
            Materiales mat = cmbMateriales.SelectedItem as Materiales;
            if (mat != null)
            {
                check.MaterialesPrestados.Add(mat);
                ActualizarListaDeMaterialesEnCheck();
            }
        }

        private void btnRestar_Click(object sender, RoutedEventArgs e)
        {
            Materiales mat = dtgMaterialesEnCheck.SelectedItem as Materiales;
            if (mat != null)
            {
                check.MaterialesPrestados.Remove(mat);
                ActualizarListaDeMaterialesEnCheck();
            }
        }

        private void btnGuardarCheck_Click(object sender, RoutedEventArgs e)
        {
            if (accionCheck == accion.Nuevo)
            {
                check.EncargadoDeAlmacen = cmbAlmacenista.SelectedItem as Empleado;
                check.FechaEntrega = dtpFechaEntrega.SelectedDate.Value;
                check.FechaHoraSolicitud = DateTime.Now;
                check.Solicitante = cmbSolicitante.SelectedItem as Empleado;
                if (manejadorcheck.Agregar(check))
                {
                    MessageBox.Show("Check guardado correctamente","", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LimpiarCamposCheck();
                    GridDetalle.IsEnabled = false;
                    ActualizarTablaCheck();
                }
                else
                {
                    MessageBox.Show("Error al guardar el check", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                check.EncargadoDeAlmacen = cmbAlmacenista.SelectedItem as Empleado;
                check.FechaEntrega = dtpFechaEntrega.SelectedDate.Value;
                check.FechaHoraSolicitud = DateTime.Now;
                check.Solicitante = cmbSolicitante.SelectedItem as Empleado;
                check.FechaEntregaReal = DateTime.Parse(lblFechaHoraEntregaReal.Content.ToString());
                if (manejadorcheck.Modificar(check))
                {
                    MessageBox.Show("Check Modificado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCheck();
                    GridDetalle.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Error al Modificar el check", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LimpiarCamposCheck()
        {
            dtpFechaEntrega.SelectedDate = DateTime.Now;
            lblFechaHoraEntregaReal.Content = "";
            lblFechaHoraPrestamo.Content = "";
            dtgMaterialesEnCheck.ItemsSource = null;
            cmbAlmacenista.ItemsSource = null;
            cmbMateriales.ItemsSource = null;
            cmbSolicitante.ItemsSource = null; 
        }

        private void dtgEnCheck_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Check v = dtgCheck.SelectedItem as Check;
            if (v != null)
            {
                check = v;
                GridDetalle.IsEnabled = true;

                ActualizarListaDeMaterialesEnCheck();
                accionCheck = accion.Editar;

                lblFechaHoraPrestamo.Content = check.FechaHoraSolicitud.ToString();
                lblFechaHoraEntregaReal.Content = check.FechaEntregaReal.ToString();

                ActualizarComboDetalle();
                cmbAlmacenista.Text = check.EncargadoDeAlmacen.ToString();
                cmbSolicitante.Text = check.Solicitante.ToString();
                dtpFechaEntrega.SelectedDate = check.FechaEntrega;
            }
    }

        private void btnEntregarCheck_Click(object sender, RoutedEventArgs e)
        {
            lblFechaHoraEntregaReal.Content = DateTime.Now;
        }

        private void btnCancelarCheck_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCheck();
            GridDetalle.IsEnabled = false;
        }

        private void btnReporteCheck_Click(object sender, RoutedEventArgs e)
        {
            Reportes reporte = new Reportes();
            reporte.Show();
        }
    }
}

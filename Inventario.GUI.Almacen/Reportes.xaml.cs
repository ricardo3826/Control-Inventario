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
using System.Windows.Shapes;


namespace Inventario.GUI.Almacen
{
    /// <summary>
    /// Interaction logic for Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {

        IManejadorDeEmpleados manejadorEmpleado;
        IManejadorDeCheck manejadorDeCheck;
        public Reportes()
        {
            InitializeComponent();
            manejadorEmpleado = new ManejadorDeEmpleados (new RepositorioDeEmpleados());
            manejadorDeCheck = new ManejadorDeCheck(new RepositorioDeChecks());
            cmbpersona.ItemsSource = manejadorEmpleado.Listar;
        }

        private void cmbpersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbpersona.SelectedItem != null)
            {
                dtgTablareportes.ItemsSource = null;
                dtgTablareportes.ItemsSource = manejadorDeCheck.BuscarNoEntregadoPorEmpleado
                    (cmbpersona.SelectedItem as Empleado);
            } 
        }
    }
}

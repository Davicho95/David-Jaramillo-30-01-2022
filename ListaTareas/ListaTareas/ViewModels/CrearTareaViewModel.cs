using ListaTareas.Models.Tablas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListaTareas.ViewModels
{
    public class CrearTareaViewModel : BaseViewModel
    {
        #region Variables Globales

        #endregion

        #region Atributos
        private bool cargando;

        private string nombre;
        private string descripcion;
        #endregion

        #region Propiedades
        public bool Cargando
        {
            get { return this.cargando; }
            set { SetValue(ref this.cargando, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }

        public string Descripcion
        {
            get { return this.descripcion; }
            set { SetValue(ref this.descripcion, value); }
        }
        #endregion

        #region Constructor
        public CrearTareaViewModel()
        {
            this.Cargando = false;
        }
        #endregion

        #region Comandos
        public ICommand CrearTareaCommand
        {
            get
            {
                return new Command(CrearTarea);
            }
        }
        #endregion

        #region Métodos
        private async void CrearTarea()
        {
            try
            {
                this.Cargando = true;
                //Valido campos obligatorios
                if (string.IsNullOrEmpty(this.Nombre))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Advertencia",
                        "El campo \"Nombre\" es obligatorio.",
                        "Aceptar");
                    this.Cargando = false;
                    return;
                }

                if (string.IsNullOrEmpty(this.Descripcion))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Advertencia",
                        "El campo \"Descripción\" es obligatorio.",
                        "Aceptar");
                    this.Cargando = false;
                    return;
                }

                Tareas tarea = new Tareas()
                {
                    Nombre = this.Nombre,
                    Descripcion = this.Descripcion,
                    Completa = false
                };

                await App.Context.SaveItemAsync<Tareas>(tarea, true);

                await Application.Current.MainPage.DisplayAlert(
                        "Mensaje",
                        "Tarea creada correctamente.",
                        "Aceptar");

                //Actualizo lista de tareas
                MainViewModel.GetInstance().Tareas.CargarListaTareas();

                //Regreso a la pantalla anterior
                await Application.Current.MainPage.Navigation.PopAsync();

                this.Cargando = false;
            }
            catch (Exception ex)
            {
                this.Cargando = false;
                return;
            }
        }
        #endregion
    }
}

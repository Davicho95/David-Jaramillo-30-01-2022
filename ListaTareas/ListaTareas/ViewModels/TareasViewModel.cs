using ListaTareas.Models.Tablas;
using ListaTareas.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListaTareas.ViewModels
{
    public class TareasViewModel : BaseViewModel
    {
        #region Variables Globales

        #endregion

        #region Atributos
        private bool estaCargando;
        private bool cargando;

        private ObservableCollection<Tareas> tareasLista;
        #endregion

        #region Propiedades
        public bool EstaCargando
        {
            get { return this.estaCargando; }
            set { SetValue(ref this.estaCargando, value); }
        }

        public bool Cargando
        {
            get { return this.cargando; }
            set { SetValue(ref this.cargando, value); }
        }

        public ObservableCollection<Tareas> TareasLista
        {
            get { return this.tareasLista; }
            set { SetValue(ref this.tareasLista, value); }
        }
        #endregion

        #region Constructor
        public TareasViewModel()
        {
            this.Cargando = false;
            this.EstaCargando = false;
            this.TareasLista = new ObservableCollection<Tareas>();
            CargarListaTareas();
        }
        #endregion

        #region Comandos
        public ICommand RecargarCommand
        {
            get
            {
                return new Command(Recargar);
            }
        }

        public ICommand EliminarTareaCommand
        {
            get
            {
                return new Command<Tareas>(EliminarTarea);
            }
        }

        public ICommand CompletarTareaCommand
        {
            get
            {
                return new Command<Tareas>(CompletarTarea);
            }
        }

        public ICommand CrearTareaCommand
        {
            get
            {
                return new Command(CrearTarea);
            }
        }
        #endregion

        #region Métodos
        private void Recargar()
        {
            try
            {
                this.EstaCargando = true;
                CargarListaTareas();
                this.EstaCargando = false;
            }
            catch (Exception ex)
            {
                this.EstaCargando = false;
                return;
            }
        }

        public async void CargarListaTareas()
        {
            try
            {
                this.TareasLista.Clear();
                List<Tareas> tareas = await App.Context.GetItemsAsync<Tareas>();

                foreach (Tareas tarea in tareas)
                {
                    this.TareasLista.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                this.EstaCargando = false;
                return;
            }
        }

        private async void EliminarTarea(Tareas tarea)
        {
            try
            {
                this.Cargando = true;
                await App.Context.DeleteItemAsync<Tareas>(tarea);
                await App.Context.SaveItemAsync<TareasEliminadas>(new TareasEliminadas
                {
                    Id = tarea.Id,
                    Descripcion = tarea.Descripcion,
                    Nombre = tarea.Nombre
                },
                true);
                this.TareasLista.Remove(tarea);
                this.Cargando = false;
            }
            catch (Exception ex)
            {
                this.Cargando = false;
                return;
            }
        }

        private async void CompletarTarea(Tareas tarea)
        {
            try
            {
                int indice = this.TareasLista.IndexOf(tarea);
                this.Cargando = true;
                tarea.Completa = true;
                await App.Context.SaveItemAsync<Tareas>(tarea, false);
                this.TareasLista[indice] = tarea;
                
                this.Cargando = false;
            }
            catch (Exception ex)
            {
                this.Cargando = false;
                return;
            }
        }

        private async void CrearTarea()
        {
            try
            {
                MainViewModel.GetInstance().CrearTarea = new CrearTareaViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new CrearTareaPage());
            }
            catch (Exception ex)
            {

                return;
            }
        }
        #endregion
    }
}

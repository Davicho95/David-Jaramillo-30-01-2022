using ListaTareas.ViewModels;
namespace ListaTareas.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public TareasViewModel Tareas { get; set; }

        public CrearTareaViewModel CrearTarea { get; set; }
        #endregion

        #region Constructores
        public MainViewModel()
        {
            intance = this;
            this.Tareas = new TareasViewModel();
        }
        #endregion

        #region Singleyton
        private static MainViewModel intance;

        public static MainViewModel GetInstance()
        {
            if (intance == null)
            {
                return new MainViewModel();
            }
            return intance;
        }
        #endregion
    }
}

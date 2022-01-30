using ListaTareas.ViewModels;

namespace ListaTareas.Infrastructure
{
    class InstanceLocator
    {
        #region Property
        public MainViewModel Main { get; set; }
        #endregion

        #region Constructore
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
